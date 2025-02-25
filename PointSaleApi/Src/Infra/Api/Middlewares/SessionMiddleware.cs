using System.Reflection.PortableExecutable;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Middlewares
{
  public class SessionMiddleware
  {
    private readonly ISessionService _sessionService;
    private readonly RequestDelegate _next;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Método principal do middleware, que lida com a sessão e a autorização.
    /// </summary>
    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      TokensDTO cookiesSession = GetCookieToken(context: httpContext);

      Dictionary<string, string> payload =
        VerifyAndRenewTokenAsync(tokens: cookiesSession, response: httpContext.Response);

      Session payloadSession = ParseDictionaryToSession(payload);

      ValidateUserRole(httpContext, payloadSession);

      if (payloadSession is SessionManager)
      {
        HandleStoreSelection(httpContext, cookiesSession, payloadSession);
      }

      httpContext.SetSession(payloadSession);
      await _next(httpContext);
    }

    public SessionMiddleware(RequestDelegate next, IJwtService jwtService, ISessionService sessionService)
    {
      _sessionService = sessionService;
      _next = next;
      _jwtService = jwtService;
    }

    /// <summary>
    /// Verifica se a rota requer uma loja selecionada.
    /// </summary>
    public bool IsStoreSelectedRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      return endpoint?.Metadata?.GetMetadata<IsStoreSelectedRoute>() != null;
    }

    /// <summary>
    /// Verifica se a rota é pública.
    /// </summary>
    public bool IsPublicRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      return endpoint?.Metadata?.GetMetadata<IsPublicRoute>() != null;
    }

    /// <summary>
    /// Verifica se a rota requer permissões de administrador.
    /// </summary>
    public bool IsAdminRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      return endpoint?.Metadata?.GetMetadata<IsAdminRoute>() != null;
    }

    /// <summary>
    /// Verifica se a rota requer permissões de funcionário.
    /// </summary>
    public bool IsEmployeeRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      return endpoint?.Metadata?.GetMetadata<IsEmployeeRoute>() != null;
    }

    /// <summary>
    /// Converte o dicionário de claims em um objeto de sessão.
    /// </summary>
    private Session ParseDictionaryToSession(Dictionary<string, string> payload)
    {
      try
      {
        UserRole role = Enum.Parse<UserRole>(payload["_role"]);
        Guid? store = payload.ContainsKey(CookiesSessionKeys.StoreToken)
          ? Guid.Parse(payload[CookiesSessionKeys.StoreToken])
          : null;

        if (role == UserRole.EMPLOYEE)
        {
          return new SessionEmployee(
            username: payload[ClaimsKeySessionEmployee.Username],
            role: role,
            storeId: store
          );
        }

        return new SessionManager(
          userId: Guid.Parse(payload[ClaimsKeySession.UserId]),
          email: payload[ClaimsKeySession.Email],
          storeId: store,
          role: role
        );
      }
      catch (Exception e)
      {
        Logger.Error(e.Message);
        throw new BadRequestException("session not found!");
      }
    }

    /// <summary>
    /// Verifica e renova o token, se necessário, e atualiza os cookies.
    /// </summary>
    public Dictionary<string, string> VerifyAndRenewTokenAsync(TokensDTO tokens, HttpResponse response)
    {
      try
      {
        string accessToken = tokens.AccessToken;
        return _jwtService.VerifyTokenAndGetClaims(accessToken);
      }
      catch (BadRequestException)
      {
        string refreshToken = tokens.RefreshToken;
        var refreshTokenClaims = _jwtService.VerifyTokenAndGetClaims(refreshToken);

        UserRole role = Enum.Parse<UserRole>(refreshTokenClaims[ClaimsKeySession.Role]);

        JwtTokensDTO newTokens;

        if (role == UserRole.EMPLOYEE)
        {
          newTokens = _sessionService.CreateSessionEmployee(
            username: int.TryParse(refreshTokenClaims[ClaimsKeySessionEmployee.Username], out int usernameInt)
              ? usernameInt
              : throw new BadRequestException("Invalid refresh token"),
            storeId: Guid.Parse(refreshTokenClaims[ClaimsKeySessionEmployee.Store]),
            role: role.ToString()
          );
        }
        else
        {
          newTokens = _sessionService.CreateSessionUser(
            userId: refreshTokenClaims[ClaimsKeySession.UserId],
            email: refreshTokenClaims[ClaimsKeySession.Email],
            role: role.ToString()
          );
        }

        var accessTokenClaims = _jwtService.VerifyTokenAndGetClaims(newTokens.AccessToken);

        response.Cookies.Append(
          key: CookiesSessionKeys.AccessToken,
          value: newTokens.AccessToken,
          options: new CookieOptions { HttpOnly = true }
        );

        return accessTokenClaims;
      }
      catch (Exception e)
      {
        Logger.Fatal(e.Message);
        throw new UnauthorizedException("A sessão está inválida");
      }
    }

    /// <summary>
    /// Obtém os tokens armazenados nos cookies.
    /// </summary>
    public TokensDTO GetCookieToken(HttpContext context)
    {
      var cookiesAccessToken = context.Request.Cookies[CookiesSessionKeys.AccessToken] ?? null;
      var cookiesRefreshToken = context.Request.Cookies[CookiesSessionKeys.RefreshToken] ?? null;
      var cookiesStoreToken = context.Request.Cookies[CookiesSessionKeys.StoreToken] ?? null;

      if (string.IsNullOrEmpty(cookiesAccessToken) || string.IsNullOrEmpty(cookiesRefreshToken))
        throw new BadRequestException("Faça o login!");

      TokensDTO tokens = new TokensDTO
      {
        AccessToken = cookiesAccessToken,
        RefreshToken = cookiesRefreshToken,
      };

      if (cookiesStoreToken != null)
      {
        tokens.StoreToken = cookiesStoreToken;
      }

      return tokens;
    }

    /// <summary>
    /// Valida o papel do usuário (role) para garantir que ele tenha permissão.
    /// </summary>
    private void ValidateUserRole(HttpContext httpContext, Session payloadSession)
    {
      if (this.IsAdminRoute(httpContext) && payloadSession.Role != UserRole.ADMIN)
        throw new BadRequestException("Usuário não tem permissão!");

      if (this.IsAdminRoute(httpContext) && payloadSession.Role == UserRole.EMPLOYEE)
        throw new BadRequestException("Usuário não tem permissão para acessar métodos do employee!");
    }

    /// <summary>
    /// Lida com a seleção da loja, garantindo que o token da loja seja válido.
    /// </summary>
    private void HandleStoreSelection(HttpContext httpContext, TokensDTO cookiesSession, Session payloadSession)
    {
      var sessionStoreToken = cookiesSession.StoreToken;

      bool isStoreSelectedRoute = IsStoreSelectedRoute(httpContext);

      if (isStoreSelectedRoute && string.IsNullOrEmpty(sessionStoreToken))
      {
        throw new BadRequestException("Selecione a loja primeiro!");
      }

      ValidateStoreToken(sessionStoreToken, payloadSession);
    }

    /// <summary>
    /// Valida o token da loja e atualiza a sessão com o ID da loja.
    /// </summary>
    private void ValidateStoreToken(string sessionStoreToken, Session payloadSession)
    {
      try
      {
        var sessionStorePayload = this._jwtService.VerifyTokenAndGetClaims(sessionStoreToken);
        if (sessionStorePayload.TryGetValue("storeId", out string? storeId))
          payloadSession.StoreId = Guid.Parse(storeId);
      }
      catch (Exception)
      {
        throw new BadRequestException("Selecione a loja novamente!");
      }
    }
  }
}