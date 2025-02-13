using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class OrdersProductsRepository(DatabaseContext context) : IOrdersProductsRepository
{
  private readonly DatabaseContext _dbContext = context;

  public async Task<OrderProduct> UpdateAsync(OrderProduct orderProduct)
  {
    var updated = _dbContext.OrderProducts.Update(orderProduct);
    await _dbContext.SaveChangesAsync();

    return updated.Entity;
  }

  public async Task<OrderProduct> SaveAsync(OrderProduct orderProduct)
  {
    var created = await _dbContext.OrderProducts.AddAsync(orderProduct);
    await _dbContext.SaveChangesAsync();

    return created.Entity;
  }

  public async Task<OrderProduct?> FindByIdAsync(Guid id)
  {
    var orderProduct = await _dbContext.OrderProducts.FindAsync(id);
    return orderProduct;
  }

  public async Task<List<OrderProduct>> FindByStoreAsync(Guid storeId)
  {
    var orderProducts = await _dbContext.OrderProducts
      .Where(x => x.StoreId == storeId)
      .OrderBy(x => x.CreatedAt)
      .ToListAsync();
    return orderProducts;
  }
}