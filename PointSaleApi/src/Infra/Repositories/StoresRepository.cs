using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories
{
  public class StoresRepository(DatabaseContext context) : IStoresRepository
  {
    private readonly DatabaseContext _context = context;

    public async Task<List<Store>> FindAllByManagerAsync(Guid managerId)
    {
      var stores = await _context.Stores.Where(s => s.ManagerId == managerId).ToListAsync() ?? [];

      return stores;
    }

    public async Task<Store?> FindOneById(Guid storeId)
    {
      var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == storeId) ?? null;
      return store;
    }

    public async Task<Store> SaveAsync(Store store)
    {
      var saved = await _context.AddAsync(store);
      await _context.SaveChangesAsync();

      return saved.Entity;
    }
  }
}
