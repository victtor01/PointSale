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
    Order order = await databaseContext.Orders.AsNoTracking()
                    .Include(o => o.Table)
                    .Include(o => o.OrderProducts)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.Options)
                    .FirstOrDefaultAsync(order => order.Id == orderId)
                  ?? null;
    return order;
  }
}