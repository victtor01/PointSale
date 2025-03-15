using Microsoft.IdentityModel.Tokens;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.utils;

public static class SessionParser
{
  public static Session DictionaryToSession(Dictionary<string, string> payload)
  {
    try
    {
      UserRole role = Enum.Parse<UserRole>(payload[ClaimsKeySessionManager.Role]);
      Guid? storeId = payload.ContainsKey(ClaimsKeySession.Store) ?
          payload[ClaimsKeySession.Store].ToGuidOrThrow() : null;

      Session session = new Session { Role = role };

      if (storeId != null)
        session.StoreId = storeId;

      return session;
    }
    catch (Exception e)
    {
      Logger.Error(e.Message);
      throw new BadRequestException("houve um erro ao tentar fazer o parser para session!");
    }
  }
}