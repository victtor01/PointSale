using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersRepository
{
  public Task<Order> SaveAsync(Order order);
}