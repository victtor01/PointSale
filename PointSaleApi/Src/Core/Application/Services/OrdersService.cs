using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersService(
  IOrdersRepository ordersRepository,
  ITablesRepository tablesRepository
) : IOrdersService
{
  private const int QUANTITY_OF_ORDERS_THAT_CAN = 2;

  private static List<Order> FindAllByStatus(List<Order> orders, OrderStatus status) =>
    [.. orders.Where(o => o.Status == status)];

  public async Task<Order> CreateAsync(CreateOrderDTO createOrderDto, Guid managerId, Guid storeId)
  {
    var tableInDB = await tablesRepository.FindByIdAsync(createOrderDto.TableId);
    if (tableInDB == null) throw new NotFoundException($"Table not found");

    List<Order> ordersInDatabase =
      await ordersRepository.FindAllByManagerAndTableAsync(managerId, createOrderDto.TableId);

    if (ordersInDatabase?.Count > 0)
    {
      List<Order> ordersWhereStatusIsCurrent = FindAllByStatus(ordersInDatabase, OrderStatus.CURRENT);
      bool limitOrders = ordersWhereStatusIsCurrent.Count >= QUANTITY_OF_ORDERS_THAT_CAN;
      if (limitOrders) throw new UnauthorizedException("limit reached on the number of orders on the table");
    }

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
}