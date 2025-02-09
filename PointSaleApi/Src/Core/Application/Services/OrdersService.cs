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
    List<Order> ordersInDatabase =
      await ordersRepository.FindAllByManagerAndTableAsync(managerId, createOrderDto.TableId);
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

  public async Task<Order> FindByIdAndManagerAsync(Guid orderId, Guid managerId)
  {
    Order? order = await ordersRepository.FindByIdAsync(orderId);

    if (order == null) throw new NotFoundException("Order not found");

    if (order.ManagerId != managerId) throw new UnauthorizedException("Invalid manager id");

    return order;
  }

  public float GetTotalPriceOfOrder(Order order)
  {
    var ordersProducts = order?.OrderProducts ?? null;
    if (ordersProducts == null) return 0;

    float totalPrice = 0;
    foreach (OrderProduct currentOrder in ordersProducts)
    {
      if (currentOrder?.Product != null ) totalPrice += currentOrder.Quantity * currentOrder.Product.Price;
      
      float priceOfOptions = currentOrder?.OptionsProducts?.Where(op => op.Product != null)
        .Select(op => op.Product!.Price)
        .Sum() ?? 0;
      
      totalPrice += priceOfOptions;
    }

    return totalPrice;
  }
}