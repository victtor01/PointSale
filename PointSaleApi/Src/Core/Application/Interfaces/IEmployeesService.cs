using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IEmployeesService
{
  public Task<List<Employee>> GetAllEmployeesAsync(Guid managerId, Guid storeId);
  public Task<Employee> CreateAsync(CreateEmployeeDTO createEmployeeDto, Guid managerId, Guid storeId);
  public Task<Employee> UpdatePositionAsync(UpdatePositionEmployeeRecord updatePositionEmployeeRecord, Guid employeeId);
  public Task<Employee> GetEmployeeByIdAsync(Guid employeeId, Guid storeId);
  public Task<Employee> UpdateEmployee(Guid employeeId, UpdateEmployeeRecord updateEmployeeRecord, Guid managerId);
}