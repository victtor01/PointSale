using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersProductsRepository
{
  public Task<OrderProduct> SaveAsync(OrderProduct orderProduct);
  public Task<OrderProduct?> FindByIdAsync(Guid id);
  public Task<List<OrderProduct>> FindByStoreWithOrderAndWithTableAsync(Guid storeId);
  public Task<OrderProduct> UpdateAsync(OrderProduct orderProduct);
}