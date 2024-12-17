using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories
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

    public async Task<Manager?> FindByIdAsync(Guid managerId)
    {
      var manager =
        await _context.Managers.FirstOrDefaultAsync(manager => manager.Id == managerId) ?? null;
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
