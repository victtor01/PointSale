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
}