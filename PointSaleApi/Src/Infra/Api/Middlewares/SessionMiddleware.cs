using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Infra.Interfaces;
using PointSaleApi.Src.Infra.utils;

namespace PointSaleApi.Src.Infra.Api.Middlewares
{
  public class SessionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ITokenValidator _tokenValidator;

    public SessionMiddleware(RequestDelegate next, ITokenValidator tokenValidator)
    {
      _next = next;
      _tokenValidator = tokenValidator;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (RouteValidator.IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      TokensDTO cookiesSession = GetCookieToken(context: httpContext);

      Dictionary<string, string> payload =
        _tokenValidator.VerifyAndRenewTokenAsync(tokens: cookiesSession, response: httpContext.Response);

      Session payloadSession = SessionParser.DictionaryToSession(payload);

      ValidateUserRole(httpContext, payloadSession);

      if (RouteValidator.IsStoreSelectedRoute(httpContext))
      {
        HandleStoreSelection(cookiesSession, payloadSession);
      }

      httpContext.SetSession(payloadSession);

      await _next(httpContext);
    }

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

    private void ValidateUserRole(HttpContext httpContext, Session payloadSession)
    {
      if (RouteValidator.IsAdminRoute(httpContext) && payloadSession.Role != UserRole.ADMIN)
        throw new BadRequestException("Usuário não tem permissão!");

      if (RouteValidator.IsEmployeeRoute(httpContext) && payloadSession.Role == UserRole.EMPLOYEE)
        throw new BadRequestException("Usuário não tem permissão para acessar métodos do employee!");
    }

    private void HandleStoreSelection(TokensDTO cookiesSession, Session payloadSession)
    {
      string? sessionStoreToken = cookiesSession.StoreToken ?? null;

      if (string.IsNullOrEmpty(sessionStoreToken))
        throw new BadRequestException("Selecione a loja primeiro!");

      payloadSession.StoreId = _tokenValidator.StoreToken(sessionStoreToken);
    }
  }
}