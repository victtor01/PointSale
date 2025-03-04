using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IEmployeesService
{
  public Task<List<Employee>> GetEmployeesAsync(Guid managerId, Guid storeId);
  public Task<Employee> CreateAsync(CreateEmployeeDTO createEmployeeDto, Guid managerId, Guid storeId);
  public Task<Employee> UpdateAsync(UpdatePositionEmployeeRecord updatePositionEmployeeRecord, Guid employeeId);
}