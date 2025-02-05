using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersService(IOrdersRepository ordersRepository) : IOrdersService
{
  public async Task<Order> CreateAsync(OrderDTO orderDTO, Guid managerId, Guid storeId, string tableIdString)
  { 
    var tableId = Guid.Parse(tableIdString);
    
    Order order = new Order
    {
      Id = Guid.NewGuid(),
      ManagerId = managerId,
      TableId = tableId,
      Status = OrderStatus.CURRENT,
      StoreId = storeId
    };

    order.LoggerJson();

    Order created = await ordersRepository.SaveAsync(order);
    
    return created;
  }
}