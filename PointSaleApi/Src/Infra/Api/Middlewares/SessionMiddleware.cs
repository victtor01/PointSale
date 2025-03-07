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

      JwtTokensDTO cookiesSession = GetCookieToken(httpContext);
      Dictionary<string, string>
        payload = tokenValidator.VerifyAndRenewTokenAsync(cookiesSession, httpContext.Response);
      
      Session session = SessionParser.DictionaryToSession(payload);
      
      ValidateUserRoleForRoute(httpContext, session);

      if (session.IsManager())
      {
        Guid userId = payload[ClaimsKeySessionManager.UserId].ToGuidOrThrow();
        SessionManager managerSession = await sessionService.CreateSessionManager(userId);
        
        if (RouteValidator.IsStoreSelectedRoute(httpContext))
        {
          string tokenStoreInCookie = getTokenStoreInCookie(httpContext);
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
        var employeeSession = await sessionService.CreateSessionEmployee(username);
        httpContext.SetSession(employeeSession);
        await _next(httpContext);
        return;
      }

      throw new BadRequestException("Nenhuma permissão encontrada");
    }

    private static string getTokenStoreInCookie(HttpContext httpContext)
    {
      string? token = httpContext.Request.Cookies[CookiesSessionKeys.StoreToken];

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

    private static void ValidateUserRoleForRoute(HttpContext httpContext, Session session)
    {
      if (RouteValidator.IsAdminRoute(httpContext) && session.Role != UserRole.ADMIN)
        throw new BadRequestException("Usuário não tem permissão!");

      if (RouteValidator.IsEmployeeRoute(httpContext) && session.Role != UserRole.EMPLOYEE)
        throw new BadRequestException("Usuário não tem permissão para acessar métodos do employee!");
    }
  }
}