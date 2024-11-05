namespace PointSaleApi.src.Core.Application.Dtos.AuthDtos
{
  public enum UserRole
  {
    ADMIN,
    EMPLOYEE,
  }

  public class Session(Guid userId, string email, UserRole role)
  {
    public Guid UserId { get; set; } = userId;
    public string Email { get; set; } = email;
    public UserRole Role { get; set; } = role;
  }
}
