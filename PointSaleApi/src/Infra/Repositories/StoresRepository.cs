using Microsoft.EntityFrameworkCore;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Database;

namespace PointSaleApi.src.Infra.Repositories
{
  public class StoresRepository(DatabaseContext context) : IStoresRepository
  {
    private readonly DatabaseContext _context = context;

    public async Task<List<Store>> FindAllByManagerAsync(Guid managerId)
    {
      var stores = await _context.Stores.Where(s => s.ManagerId == managerId).ToListAsync() ?? [];

      return stores;
    }

    public async Task<Store> SaveAsync(Store store)
    {
      var saved = await _context.AddAsync(store);
      await _context.SaveChangesAsync();

      return saved.Entity;
    }
  }
}