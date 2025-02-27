using Microsoft.IdentityModel.Tokens;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Infra.utils;

public static class SessionParser
{
  public static Session DictionaryToSession(Dictionary<string, string> payload)
  {
    try
    {
      UserRole role = Enum.Parse<UserRole>(payload[ClaimsKeySession.Role]);
      
      if (role == UserRole.EMPLOYEE)
      {
        return new SessionEmployee(
          username: payload[ClaimsKeySessionEmployee.Username],
          role: role
        );
      }

      return new SessionManager(
        userId: Guid.Parse(payload[ClaimsKeySession.UserId]),
        email: payload[ClaimsKeySession.Email],
        role: role
      );
    }
    catch (Exception e)
    {
      Logger.Error(e.Message);
      throw new BadRequestException("houve um erro ao tentar fazer o parser para session!");
    }
  }
}