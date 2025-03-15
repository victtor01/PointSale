using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IEmployeeRepository
{
  public Task<Employee> AddAsync(Employee employee);
  public Task<Employee> UpdateAsync(Employee employee);
  public Task<List<Employee>> GetAllByManagerAndStoreAsync(Guid managerId, Guid storeId);
  public Task<Employee?> FindByIdAsync(Guid id);
  public Task<Employee?> FindByIdTracking(Guid id);
  public Task<Employee?> FindByUsernameAsyncWithPositions(int username);
}