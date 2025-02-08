using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class OrdersProductsRepository(DatabaseContext context) : IOrdersProductsRepository
{
  private readonly DatabaseContext _dbContext = context;
  
  public async Task<OrderProduct> AddAsync(OrderProduct orderProduct)
  {
    var created = await _dbContext.OrderProducts.AddAsync(orderProduct);
    await _dbContext.SaveChangesAsync();
    
    return created.Entity;
  }
}