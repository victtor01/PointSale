namespace PointSaleApi.Src.Core.Domain;

public enum UserRole
{
  ADMIN,
  EMPLOYEE
}

public class SessionManager : Session
{
  public Guid UserId { get; }
  public string Email { get; }
    
  public SessionManager(Guid userId, string email, UserRole role, Guid? storeId)
  {
    UserId = userId;
    Email = email;
    Role = role;
    StoreId = storeId;
  }
}