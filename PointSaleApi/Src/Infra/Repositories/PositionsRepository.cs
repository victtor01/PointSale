using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;
using PointSaleApi.Src.Infra.Interfaces;

namespace PointSaleApi.Src.Infra.Repositories;

public class PositionsRepository(DatabaseContext databaseContext) : IPositionsRepository
{
  private readonly DatabaseContext _databaseContext = databaseContext;

  public async Task<EmployeePosition> Create(EmployeePosition employeePosition)
  {
    var add = await _databaseContext.EmployeePositions.AddAsync(employeePosition);
    _databaseContext.SaveChanges();
    return add.Entity;
  }

  public async Task<EmployeePosition?> GetById(Guid id)
    => await _databaseContext.EmployeePositions.FirstOrDefaultAsync(e => e.Id == id) ?? null;

  public async Task<List<EmployeePosition>> FindAllByIds(List<Guid> ids)
  {
    return await _databaseContext.EmployeePositions
      .Where(ep => ids.Contains(ep.Id))
      .ToListAsync();
  }

  public async Task<List<EmployeePosition>> FindAllByManagerAndStoreIdAsync(Guid managerId, Guid storeId)
  {
    return await _databaseContext.EmployeePositions
      .Where(e => e.ManagerId == managerId && e.StoreId == storeId)
      .Include(p => p.Employees)
      .ToListAsync();
  }

  public async Task<EmployeePosition?> FindByNameAndManagerAsync(string name, Guid managerId)
  {
    return await _databaseContext.EmployeePositions.FirstOrDefaultAsync(e =>
      e.Name == name && e.ManagerId == managerId) ?? null;
  }

  public async Task<EmployeePosition> Update(EmployeePosition employeePosition)
  {
    _databaseContext.Update(employeePosition);
    await _databaseContext.SaveChangesAsync();

    return employeePosition;
  }
}