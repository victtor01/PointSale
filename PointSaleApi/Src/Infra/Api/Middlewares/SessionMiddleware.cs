using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Infra.Interfaces;
using PointSaleApi.Src.Infra.utils;

namespace PointSaleApi.Src.Infra.Api.Middlewares
{
  public class SessionMiddleware(RequestDelegate next)
  {
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext,
      ISessionService sessionService,
      ITokenValidator tokenValidator,
      IJwtService jwtService
    )
    {
      if (RouteValidator.IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      httpContext.Request.Cookies.LoggerJson();

      JwtTokensDTO cookiesSession = GetCookieToken(httpContext);

      Dictionary<string, string>
        payload = tokenValidator.VerifyAndRenewTokenAsync(cookiesSession, httpContext.Response);

      Session session = SessionParser.DictionaryToSession(payload);
      session.ValidateUserRoleForRoute(httpContext);

      if (session.IsManager())
      {
        Guid userId = payload[ClaimsKeySessionManager.UserId].ToGuidOrThrow();
        SessionManager managerSession = await sessionService.CreateSessionManager(userId);

        if (RouteValidator.IsStoreSelectedRoute(httpContext))
        {
          string tokenStoreInCookie = GetTokenStoreInCookie(httpContext);
          Guid decodedTokenStore = tokenValidator.GetStoreInToken(tokenStoreInCookie);
          managerSession.StoreId = decodedTokenStore;
        }

        httpContext.SetSession(managerSession);
        await _next(httpContext);

        return;
      }

      if (session.IsEmployee())
      {
        int username = payload[ClaimsKeySessionEmployee.Username].ToIntOrThrow();
        SessionEmployee employeeSession = await sessionService.CreateSessionEmployee(username);
        IEnumerable<string> requiredPermissions = GetRequiredPermissionsForRoute(httpContext);

        if (!requiredPermissions.All(permission => employeeSession.Positions.Any(position => position.Permissions.Contains(permission))))
        {
          throw new UnauthorizedException("Você não tem permissão para acessar essa rota.");
        }

        httpContext.SetSession(employeeSession);
        await _next(httpContext);
        return;
      }

      throw new BadRequestException("Nenhuma permissão encontrada");
    }

    private static string GetTokenStoreInCookie(HttpContext httpContext)
    {
      string? token = httpContext.Request.Cookies[CookiesSessionKeys.StoreToken];
      Console.WriteLine(token);
      httpContext.Request.Cookies.LoggerJson();
      if (string.IsNullOrEmpty(token))
        throw new BadRequestException("Faça o login!");

      return token;
    }

    private static JwtTokensDTO GetCookieToken(HttpContext context)
    {
      var accessToken = context.Request.Cookies[CookiesSessionKeys.AccessToken];
      var refreshToken = context.Request.Cookies[CookiesSessionKeys.RefreshToken];

      if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        throw new BadRequestException("Faça o login!");

      return new JwtTokensDTO
      {
        AccessToken = accessToken,
        RefreshToken = refreshToken
      };
    }

    private IEnumerable<string> GetRequiredPermissionsForRoute(HttpContext context)
    {
      var endpoint = context.GetEndpoint();
      if (endpoint == null)
      {
        return Enumerable.Empty<string>();
      }

      // Lista para armazenar as permissões
      var permissions = new List<string>();

      // Pega os atributos de permissão da rota atual
      var permissionAttributes = endpoint.Metadata
        .OfType<PermissionAttribute>(); // Pega todos os atributos que herdam de PermissionAttribute

      foreach (var attribute in permissionAttributes)
      {
        permissions.AddRange(attribute.Permissions); // Adiciona as permissões do atributo
      }

      return permissions;
    }
  }
}