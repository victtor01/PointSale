using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

public class SessionEmployee : Session
{
  public int Username { get; set; }
  public Guid managerId { get; set; }
  
  public SessionEmployee(int username, UserRole role)
  {
    Username = username;
    Role = role;
  }
}