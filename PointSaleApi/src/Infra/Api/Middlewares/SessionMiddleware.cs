using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.Src.Core.Application.Interfaces.AuthInterfaces;
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

    public Session ParseDictionarySessionToSession(Dictionary<string, string> payload)
    {
      try
      {
        UserRole role = Enum.Parse<UserRole>(payload["_role"]);
        Session createSession =
          new(userId: Guid.Parse(payload["userId"]), email: payload["_email"], role: role);

        return createSession;
      }
      catch (Exception)
      {
        throw new BadRequestException("session not found!");
      }
    }

    public Dictionary<string, string> VerifyAndRenewTokenAsync(
      JwtTokensDto tokens,
      HttpResponse response
    )
    {
      string accessToken = tokens.AccessToken;
      string refreshToken = tokens.RefreshToken;

      try
      {
        return _jwtService.VerifyTokenAndGetClaims(accessToken);
      }
      catch (BadRequestException)
      {
        var refreshTokenClaims = _jwtService.VerifyTokenAndGetClaims(refreshToken);

        JwtTokensDto newTokens = _sessionService.CreateSessionUser(
          userId: refreshTokenClaims[ClaimKeysSession.UserId],
          email: refreshTokenClaims[ClaimKeysSession.Email],
          role: refreshTokenClaims[ClaimKeysSession.Role]
        );

        string newAccessToken = newTokens.AccessToken;

        var accessTokenClaims = _jwtService.VerifyTokenAndGetClaims(newAccessToken);

        response.Cookies.Append(
          key: CookiesSessionKeys.AccessToken,
          value: newTokens.AccessToken,
          options: new CookieOptions { HttpOnly = true }
        );

        return accessTokenClaims;
      }
      catch (Exception)
      {
        throw new UnauthorizedException("The session is not valid!");
      }
    }

    public JwtTokensDto GetCookieToken(HttpContext context)
    {
      var cookiesAccessToken = context.Request.Cookies[CookiesSessionKeys.AccessToken] ?? null;
      var cookiesRefreshToken = context.Request.Cookies[CookiesSessionKeys.RefreshToken] ?? null;

      if (string.IsNullOrEmpty(cookiesAccessToken) || string.IsNullOrEmpty(cookiesRefreshToken))
        throw new BadRequestException("Faça o login!");

      return new JwtTokensDto
      {
        AccessToken = cookiesAccessToken,
        RefreshToken = cookiesRefreshToken,
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

      Logger.Error(cookiesSession.AccessToken);

      string AccessToken = cookiesSession.AccessToken;

      Dictionary<string, string> payload = VerifyAndRenewTokenAsync(
        cookiesSession,
        httpContext.Response
      );

      Session payloadSession = ParseDictionarySessionToSession(payload);

      if (IsAdminRoute(httpContext) && payloadSession.Role != UserRole.ADMIN)
        throw new BadRequestException("user not have permission!");

      httpContext.SetSession(payloadSession);

      await _next(httpContext);
    }
  }
}
