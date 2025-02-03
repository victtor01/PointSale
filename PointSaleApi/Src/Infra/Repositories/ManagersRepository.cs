using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class ManagersRepository(DatabaseContext context) : IManagersRepository
{
  public async Task<Manager?> FindByEmailAsync(string email)
  {
    var manager =
      await context.Managers.AsNoTracking().FirstOrDefaultAsync(manager => manager.Email == email) ?? null;

    return manager;
  }

  public async Task<Manager?> FindByIdAsync(Guid managerId)
  {
    var manager =
      await context.Managers.FirstOrDefaultAsync(manager => manager.Id == managerId) ?? null;
    return manager;
  }

  public async Task<Manager> SaveAsync(Manager manager)
  {
    var savedManager = await context.Managers.AddAsync(manager);
    await context.SaveChangesAsync();

    return savedManager.Entity;
  }
}