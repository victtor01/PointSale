using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Infra.Interfaces;
using PointSaleApi.Src.Infra.utils;

namespace PointSaleApi.Src.Infra.Api.Middlewares
{
  public class SessionMiddleware(RequestDelegate next)
  {
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext, ISessionService sessionService,
      ITokenValidator _tokenValidator)
    {
      if (RouteValidator.IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      TokensManagerDTO cookiesSession = GetCookieToken(context: httpContext);

      Dictionary<string, string> payload =
        _tokenValidator.VerifyAndRenewTokenAsync(tokens: cookiesSession, response: httpContext.Response);

      Session payloadSession = SessionParser.DictionaryToSession(payload);

      IsValidUserRoleToRoute(httpContext, payloadSession);

      switch (payloadSession.Role)
      {
        case UserRole.EMPLOYEE:
        {
          int username = payload[ClaimsKeySessionEmployee.Username].ToIntOrThrow();
          SessionEmployee employeeSession = await sessionService.CreateSessionEmployee(username);
          httpContext.SetSession(employeeSession);
          break;
        }
        case UserRole.ADMIN:
        {
          Guid userID = payload[ClaimsKeySessionManager.UserId].ToGuidOrThrow();
          SessionManager employeeSession = await sessionService.CreateSessionManager(userID);
          httpContext.SetSession(employeeSession);
          break;
        }

        default:
          throw new BadRequestException("Nenhuma permissão encontrada");
      }

      await _next(httpContext);
    }

    public TokensManagerDTO GetCookieToken(HttpContext context)
    {
      string? cookiesAccessToken = context.Request.Cookies[CookiesSessionKeys.AccessToken] ?? null;
      string? cookiesRefreshToken = context.Request.Cookies[CookiesSessionKeys.RefreshToken] ?? null;
      string? cookiesStoreToken = context.Request.Cookies[CookiesSessionKeys.StoreToken] ?? null;

      if (string.IsNullOrEmpty(cookiesAccessToken) || string.IsNullOrEmpty(cookiesRefreshToken))
        throw new BadRequestException("Faça o login!");

      TokensManagerDTO tokens = new TokensManagerDTO
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

    private static void IsValidUserRoleToRoute(HttpContext httpContext, Session payloadSession)
    {
      if (RouteValidator.IsAdminRoute(httpContext) && payloadSession.Role != UserRole.ADMIN)
        throw new BadRequestException("Usuário não tem permissão!");

      if (RouteValidator.IsEmployeeRoute(httpContext) && payloadSession.Role == UserRole.EMPLOYEE)
        throw new BadRequestException("Usuário não tem permissão para acessar métodos do employee!");
    }
  }
}