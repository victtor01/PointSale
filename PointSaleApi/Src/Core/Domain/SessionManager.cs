namespace PointSaleApi.Src.Core.Domain;

public enum UserRole
{
  ADMIN,
  EMPLOYEE
}

public class SessionManager : Session
{
  public required Guid UserId { get; set; }
  public required string Email { get; set;  }

  public SessionManager()
  {
    Role = UserRole.ADMIN;
  }  
}