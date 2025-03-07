using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class StoresRepository(DatabaseContext context) : IStoresRepository
{
  private readonly DatabaseContext _context = context;

  public async Task<List<Store>> FindAllByManagerAsync(Guid managerId)
  {
    var stores = await _context.Stores
      .AsNoTracking()
      .Where(s => s.ManagerId == managerId)
      .Include(S => S.Tables)
      .ToListAsync() ?? [];

    return stores;
  }


  public async Task<Store?> FindOneById(Guid storeId)
  {
    var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == storeId) ?? null;
    return store;
  }

  public async Task<Store?> FindByIdWithTablesWithOrdersAndProductsAsync(Guid storeId)
  {
    return await _context.Stores
      .AsNoTracking()
      .Include(store => store.Tables)
      .Include(store => store.Orders)
      .ThenInclude(order => order.OrderProducts)
      .ThenInclude(orderProduct => orderProduct.Product)
      .FirstOrDefaultAsync(s => s.Id == storeId) ?? null;
  }

  public async Task<List<Store>> FindAllByManagerIdWithOrdersAsync(Guid managerId)
  {
    return await _context.Stores
      .AsNoTracking()
      .Include(store => store.Tables)
      .Include(store => store.Orders)
      .ThenInclude(order => order.OrderProducts)
      .ThenInclude(orderProduct => orderProduct.Product)
      .ToListAsync();
    
  }

  public async Task<Store> SaveAsync(Store store)
  {
    var saved = await _context.AddAsync(store);
    await _context.SaveChangesAsync();

    return saved.Entity;
  }
}