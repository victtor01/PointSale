using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersRepository
{
  public Task<Order> SaveAsync(Order order);
  public Task<List<Order>> FindAllByStatusAsync(OrderStatus status);
  public Task<List<Order>> FindAllByManagerAndTableAsync(Guid managerId, Guid tableId);
}