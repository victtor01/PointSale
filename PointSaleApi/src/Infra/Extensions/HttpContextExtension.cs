using PointSaleApi.src.Core.Application.Dtos.AuthDtos;

namespace PointSaleApi.src.Infra.Extensions
{
  public static class HttpContextExtensions
  {
    private const string SessionKey = "session";

    public static Session GetSession(this HttpContext context)
    {
      if (context.Items[SessionKey] is Session session)
        return session;

      throw new UnauthorizedAccessException(
        "A sessão pode estar expirada, tente fazer o login novamente!"
      );
    }

    public static void SetSession(this HttpContext context, Session session)
    {
      context.Items[SessionKey] = session;
    }
  }
}
