using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersService(IOrdersRepository ordersRepository) : IOrdersService
{
  private const int QUANTITY_OF_ORDERS_THAT_CAN = 2;

  private List<Order> FindAllByStatus(List<Order> orders, OrderStatus status) =>
    orders.Where(o => o.Status == status).ToList();

  public async Task<Order> CreateAsync(CreateOrderDTO createOrderDto, Guid managerId, Guid storeId)
  {
    List<Order> ordersInDatabase = await ordersRepository.FindAllByManagerAndTableAsync(managerId, createOrderDto.TableId);
    List<Order> ordersWhereStatusIsCurrent = this.FindAllByStatus(ordersInDatabase, OrderStatus.CURRENT);

    bool limitOrders = ordersWhereStatusIsCurrent.Count >= QUANTITY_OF_ORDERS_THAT_CAN;
    if (limitOrders) throw new UnauthorizedException("limit reached on the number of orders on the table");

    Order order = new Order
    {
      Id = Guid.NewGuid(),
      ManagerId = managerId,
      TableId = createOrderDto.TableId,
      Status = OrderStatus.CURRENT,
      StoreId = storeId
    };

    Order created = await ordersRepository.SaveAsync(order);

    return created;
  }

  public async Task<List<Order>> FindAllByTableIdAndManagerAsync(Guid managerId, Guid tableId)
  {
    List<Order> orders = await ordersRepository.FindAllByManagerAndTableAsync(managerId, tableId);
    return orders;
  }
}