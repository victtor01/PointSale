namespace PointSaleApi.Src.Core.Domain;

public class Session
{
  public UserRole Role { get; set; }
  public Guid? StoreId { get; set; }

  public bool IsManager () => Role == UserRole.ADMIN;
  public bool IsEmployee () => Role == UserRole.EMPLOYEE;
}