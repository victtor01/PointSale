using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Attributes;
using PointSaleApi.src.Infra.Config;
using PointSaleApi.src.Infra.Extensions;

namespace PointSaleApi.src.Infra.Api.Middlewares
{
  public class SessionMiddleware(RequestDelegate next, IJwtService jwtService)
  {
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

    public string GetCookieToken(HttpContext context)
    {
      var cookiesAccessToken = context.Request.Cookies[CookiesNames.AccessToken] ?? null;

      if (string.IsNullOrEmpty(cookiesAccessToken))
        throw new BadRequestException("Faça o login!");

      return cookiesAccessToken;
    }

    public Session ParseSession(Dictionary<string, string> payload)
    {
      try
      {
        UserRole role = Enum.Parse<UserRole>(payload["role"]);
        Session createSession =
          new(userId: Guid.Parse(payload["userId"]), email: payload["email"], role: role);

        return createSession;
      }
      catch (System.Exception)
      {
        throw new BadHttpRequestException("session not found!");
      }
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
      if (IsPublicRoute(httpContext))
      {
        await _next(httpContext);
        return;
      }

      string cookiesAccessToken = GetCookieToken(httpContext);
      var payload = _jwtService.DecodeTokenAndGetClaims(cookiesAccessToken);

      Session payloadSession = ParseSession(payload);

      if (IsAdminRoute(httpContext) && payloadSession.Role != UserRole.ADMIN)
      {
        throw new BadRequestException("user not have permission!");
      }

      httpContext.SetSession(payloadSession);

      await _next(httpContext);
    }
  }
}
