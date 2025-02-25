using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersService
{
  public Task<Order> CreateAsync(CreateOrderDTO createOrderDto, Guid managerId, Guid storeId);
}

public interface IFindOrdersService
{
  public Task<Order> ByIdAndManagerAsync(Guid orderId, Guid managerId);
  public Task<List<Order>> ByManagerAndStoreAsync(Guid managerId, Guid storeId);
}