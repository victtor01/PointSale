using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersProductsRepository
{
  public Task<OrderProduct> AddAsync(OrderProduct orderProduct);
}