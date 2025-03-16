using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class EmployeesRepository(DatabaseContext context) : IEmployeeRepository
{
  public async Task<Employee> AddAsync(Employee employee)
  {
    var created = await context.Employees.AddAsync(employee);
    await context.SaveChangesAsync();
    return created.Entity;
  }

  public async Task<List<Employee>> GetAllByManagerAndStoreAsync(Guid managerId, Guid storeId)
  {
    List<Employee> employees = await context.Employees
      .Where(emp => emp.ManagerId == managerId && emp.StoreId == storeId)
      .Include(emp => emp.Positions)
      .ToListAsync();

    return employees;
  }

  public async Task<List<Employee>> GetAllByIds(List<Guid> ids)
  {
    List<Employee> employees = await context.Employees
      .Where(emp => ids.Contains(emp.Id))
      .ToListAsync();

    return employees;
  }

  public async Task<Employee?> FindByUsernameAsyncWithPositions(int username)
  {
    var employee = await context.Employees
      .AsNoTracking()
      .Include(e => e.Positions)
      .FirstOrDefaultAsync(emp => emp.Username == username) ?? null;

    return employee;
  }

  public async Task<Employee?> FindByIdAsync(Guid id)
  {
    return await context.Employees
      .AsNoTracking()
      .Include(e => e.Positions)
      .FirstOrDefaultAsync(e => e.Id == id) ?? null;
  }

  public async Task<Employee?> FindByIdTracking(Guid id)
  {
    return await context.Employees
      .Include(e => e.Positions)
      .FirstOrDefaultAsync(e => e.Id == id) ?? null;
  }

  public async Task<Employee> UpdateAsync(Employee employee)
  {
    context.Entry(employee).State = EntityState.Modified;
    await context.SaveChangesAsync();

    return employee;
  }
}