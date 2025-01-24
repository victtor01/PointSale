using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Infra.Extensions
{
  public static class GetStoreSessionOrThrow
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

    public static Guid GetStoreOrthrow(this HttpContext context)
    {
      var contextSession =
        (context.Items[SessionKey] ?? null) as Session
        ?? throw new BadRequestException("Sessão inválida na requisição");

      return contextSession.StoreId
        ?? throw new UnauthorizedException("Formato da sessão inválida");
    }

    public static void SetSession(this HttpContext context, Session session)
    {
      context.Items[SessionKey] = session;
    }
  }
}
