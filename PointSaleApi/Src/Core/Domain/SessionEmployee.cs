using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

public class SessionEmployee : Session
{
  public int Username { get; set; }

  public SessionEmployee(string username, UserRole role)
  {
    Username = int.TryParse(username, out int userId)
      ? userId
      : throw new BadRequestException("username of session is invalid");
    Role = role;
  }
}