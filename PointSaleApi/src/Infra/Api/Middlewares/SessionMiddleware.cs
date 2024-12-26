using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.Src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.SessionInterfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Middlewares
{
  public class SessionMiddleware(
    RequestDelegate next,
    IJwtService jwtService,
    ISessionService sessionService
  )
  {
    private readonly ISessionService _sessionService = sessionService;
    private readonly RequestDelegate _next = next;
    private readonly IJwtService _jwtService = jwtService;

    public bool IsStoreSelectedRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      var isStoreSelectedRoute = endpoint?.Metadata?.GetMetadata<IsStoreSelectedRoute>() != null;
      return isStoreSelectedRoute;
    }

    public bool IsPublicRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      var isPublicRoute = endpoint?.Metadata?.GetMetadata<IsPublicRoute>() != null;
      return isPublicRoute;
    }

    public bool IsAdminRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      var isAdminRoute = endpoint?.Metadata?.GetMetadata<IsAdminRoute>() != null;
      return isAdminRoute;
    }

    public Session ParseDictionaryToSession(Dictionary<string, string> payload)
    {
      try
      {
        UserRole role = Enum.Parse<UserRole>(payload["_role"]);
        Guid? store = payload.ContainsKey(CookiesSessionKeys.StoreToken)
          ? Guid.Parse(payload[CookiesSessionKeys.StoreToken])
          : null;

        Session createSession =
          new(
            userId: Guid.Parse(payload["userId"]),
            email: payload["_email"],
            storeId: store,
            role: role
          );

        return createSession;
      }
      catch (Exception e)
      {
        Logger.Error(e.Message);
        throw new BadRequestException("session not found!");
      }
    }

    public Dictionary<string, string> VerifyAndRenewTokenAsync(
      JwtTokensDto tokens,
      HttpResponse response
    )
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
        JwtTokensDto newTokens = _sessionService.CreateSessionUser(
          userId: refreshTokenClaims[ClaimKeysSession.UserId],
          email: refreshTokenClaims[ClaimKeysSession.Email],
          role: refreshTokenClaims[ClaimKeysSession.Role]
        );

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

    public JwtTokensDto GetCookieToken(HttpContext context)
    {
      var cookiesAccessToken = context.Request.Cookies[CookiesSessionKeys.AccessToken] ?? null;
      var cookiesRefreshToken = context.Request.Cookies[CookiesSessionKeys.RefreshToken] ?? null;
      var cookiesStoreToken = context.Request.Cookies[CookiesSessionKeys.StoreToken] ?? null;

      if (string.IsNullOrEmpty(cookiesAccessToken) || string.IsNullOrEmpty(cookiesRefreshToken))
        throw new BadRequestException("Faça o login!");

      return new JwtTokensDto
      {
        AccessToken = cookiesAccessToken,
        RefreshToken = cookiesRefreshToken,
        StoreToken = cookiesStoreToken,
      };
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      JwtTokensDto cookiesSession = GetCookieToken(httpContext);
      var payload = this.VerifyAndRenewTokenAsync(cookiesSession, httpContext.Response);
      Session payloadSession = ParseDictionaryToSession(payload);

      if (IsAdminRoute(httpContext) && payloadSession.Role != UserRole.ADMIN)
        throw new BadRequestException("usuário não tem permissão!");

      var isStoreSelectedRoute = IsStoreSelectedRoute(httpContext);
      var sessionStoreToken = cookiesSession.StoreToken ?? null;
      if (isStoreSelectedRoute && string.IsNullOrEmpty(sessionStoreToken))
        throw new BadRequestException("selecione a loja primeiro!");

      if (isStoreSelectedRoute)
      {
        try
        {
          var sessionStorePayload = this._jwtService.VerifyTokenAndGetClaims(sessionStoreToken!);
          if (sessionStorePayload.TryGetValue("storeId", out string? storeId))
            payloadSession.StoreId = Guid.Parse(storeId);
        }
        catch (Exception)
        {
          throw new BadRequestException("Selecione a loja novamente!");
        }
      }

      httpContext.SetSession(payloadSession);

      await _next(httpContext);
    }
  }
}
