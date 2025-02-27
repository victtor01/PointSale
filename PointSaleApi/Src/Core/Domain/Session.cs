namespace PointSaleApi.Src.Core.Domain;

public abstract class Session
{
  public UserRole Role { get; set; }
  public Guid? StoreId { get; set; }
}