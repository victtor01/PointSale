using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Infra.Extensions;

public static class GetStoreSessionOrThrow
{
  private const string SessionKey = "session";

  public static SessionManager GetManagerSessionOrThrow(this HttpContext context)
  {
    if (context.Items[SessionKey] is SessionManager session)
      return session;

    throw new UnauthorizedAccessException(
      "A sessão pode estar expirada, tente fazer o login novamente!"
    );
  }

  public static SessionEmployee GetEmployeeSessionOrThrow(this HttpContext context)
  {
    if (context.Items[SessionKey] is SessionEmployee session)
      return session;

    throw new UnauthorizedAccessException(
      "A sessão pode estar expirada, tente fazer o login novamente!"
    );
  }

  public static Guid GetStoreIdOrThrow(this HttpContext context)
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