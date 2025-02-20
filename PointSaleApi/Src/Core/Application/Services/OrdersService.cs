using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersService(IOrdersRepository ordersRepository, ITablesRepository tablesRepository) : IOrdersService
{
  private const int QUANTITY_OF_ORDERS_THAT_CAN = 2;

  private List<Order> FindAllByStatus(List<Order> orders, OrderStatus status) => 
    orders.Where(o => o.Status == status).ToList();

  public async Task<Order> CreateAsync(CreateOrderDTO createOrderDto, Guid managerId, Guid storeId)
  {
    var tableInDB = await tablesRepository.FindByIdAsync(createOrderDto.TableId);
    if (tableInDB == null) throw new NotFoundException($"Table not found");

    List<Order> ordersInDatabase =
      await ordersRepository.FindAllByManagerAndTableAsync(managerId, createOrderDto.TableId);

    if (ordersInDatabase?.Count > 0)
    {
      List<Order> ordersWhereStatusIsCurrent = this.FindAllByStatus(ordersInDatabase, OrderStatus.CURRENT);
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

  public async Task<List<Order>> GetAllAsync(Guid managerId, Guid storeId)
  {
    var orders = await ordersRepository.FindAllByCreatedDateAsync(managerId, storeId);
    orders.ForEach(order => order.OrderProducts = order.OrderProducts.OrderBy(op => op?.CreatedAt).ToList());
    return orders;
  }

  public async Task<Order> FindByIdAndManagerAsync(Guid orderId, Guid managerId)
  {
    Order? order = await ordersRepository.FindByIdAsync(orderId);

    if (order == null || order.ManagerId != managerId)
      throw new NotFoundException("Order not found");

    return order;
  }

  public float GetTotalPriceOfOrder(Order order)
  {
    var ordersProducts = order?.OrderProducts ?? null;
    if (ordersProducts == null || ordersProducts.Count == 0) return 0;

    float totalPrice = 0;
    foreach (var currentOrder in ordersProducts)
    {
      if (currentOrder?.Product != null) totalPrice += currentOrder.Quantity * currentOrder.Product.Price;

      float priceOfOptions = currentOrder?.OptionsProducts?.Where(op => op.Product != null)
        .Select(op => op.Product!.Price)
        .Sum() ?? 0;

      totalPrice += priceOfOptions;
    }

    return totalPrice;
  }
}