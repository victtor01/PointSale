using Microsoft.EntityFrameworkCore;
using PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Database;

namespace PointSaleApi.src.Infra.Repositories
{
  public class ManagersRepository(DatabaseContext context) : IManagersRepository
  {
    private readonly DatabaseContext _context = context;

    public async Task<Manager?> FindByEmailAsync(string email)
    {
      var manager =
        await _context.Managers.FirstOrDefaultAsync(manager => manager.Email == email) ?? null;

      return manager;
    }

    public async Task<Manager> Save(Manager manager)
    {
      var savedManager = await _context.Managers.AddAsync(manager);
      await _context.SaveChangesAsync();

      return savedManager.Entity;
    }
  }
}
