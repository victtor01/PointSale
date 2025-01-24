namespace PointSaleApi.Src.Core.Application.Dtos.AuthDtos
{
  public enum UserRole
  {
    ADMIN,
    EMPLOYEE,
  }

  public class Session(Guid userId, string email, UserRole role, Guid? storeId)
  {
    public Guid UserId { get; set; } = userId;
    public string Email { get; set; } = email;
    public Guid? StoreId { get; set; } = storeId;
    public UserRole Role { get; set; } = role;
  }
}
