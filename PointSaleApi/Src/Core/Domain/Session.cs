namespace PointSaleApi.Src.Core.Domain;

public abstract class Session
{
  public Guid? StoreId { get; set; }
  public UserRole Role { get; set; }
}