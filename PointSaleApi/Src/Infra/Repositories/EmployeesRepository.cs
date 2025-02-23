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

  public async Task<Employee?> FindByIdAsync(Guid id)
  {
    return await context.Employees.FirstOrDefaultAsync(e => e.Id == id) ?? null;
  }

  public Task<Employee> UpdateAsync(Employee employee)
  {
    throw new NotImplementedException();
  }
}