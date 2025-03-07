using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class OrdersRepository(DatabaseContext databaseContext) : IOrdersRepository
{
  public async Task<Order> SaveAsync(Order order)
  {
    var saved = await databaseContext.Orders.AddAsync(order);
    await databaseContext.SaveChangesAsync();

    return saved.Entity;
  }

  public async Task<List<Order>> FindAllByStatusAsync(OrderStatus status)
  {
    List<Order> orders =
      await databaseContext.Orders.AsNoTracking().Where(order => order.Status == status)
        .ToListAsync();

    return orders;
  }

  public async Task<List<Order>> FindAllByManagerAndTableAsync(Guid managerId, Guid tableId)
  {
    List<Order> orders =
      await databaseContext.Orders.AsNoTracking()
        .Include(order => order.Table)
        .Where(order => order.TableId == tableId && order.ManagerId == managerId)
        .ToListAsync();

    return orders;
  }

  public async Task<Order?> FindByIdAsync(Guid orderId)
  {
    var order = await databaseContext.Orders
      .Include(o => o.Table)
      .Include(o => o.OrderProducts)
      .ThenInclude(orderProduct => orderProduct.Product)
      .ThenInclude(product => product!.Options)
      .FirstOrDefaultAsync(order => order.Id == orderId) ?? null;

    return order;
  }

  public async Task<List<Order>> FindAllByCreatedDateAsync(Guid managerId, Guid storeId)
  {
    List<Order> orders = await databaseContext.Orders.AsNoTracking()
      .Where(order => order.ManagerId == managerId && order.StoreId == storeId)
      .OrderBy(order => order.CreatedAt)
      .Include(order => order.OrderProducts)
      .ThenInclude(order => order.Product)
      .Include(order => order.Table)
      .ToListAsync();

    return orders;
  }
}