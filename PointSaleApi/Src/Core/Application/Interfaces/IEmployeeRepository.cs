using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IEmployeeRepository
{
  public Task<Employee> AddAsync(Employee employee);
  public Task<Employee> UpdateAsync(Employee employee);
  public Task<Employee?> FindByIdAsync(Guid id);
  
}