using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersService
{
  public Task<Order> CreateAsync(CreateOrderDTO createOrderDto, Guid managerId, Guid storeId);
  public Task<Order> FindByIdAndManagerAsync(Guid orderId, Guid managerId);
  public float GetTotalPriceOfOrder(Order orders);
  public Task<List<Order>> GetAllAsync(Guid managerId);
}