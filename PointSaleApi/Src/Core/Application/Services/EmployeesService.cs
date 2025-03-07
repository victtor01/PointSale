using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class EmployeesService(IEmployeeRepository employeeRepository) : IEmployeesService
{
  private readonly IEmployeeRepository _employeeRepository = employeeRepository;

  private const int COUNT_EMPLOYEE_MAX = 10;

  public async Task<Employee> CreateAsync(CreateEmployeeDTO createEmployeeDto, Guid managerId, Guid storeId)
  {
    List<Employee> employeesInDatabase = await _employeeRepository
      .GetAllByManagerAndStoreAsync(managerId, storeId);

    if (employeesInDatabase.Count > COUNT_EMPLOYEE_MAX)
      throw new BadRequestException("count of employees in database exceeded");

    const int BIG_SALARY = 1000 * 1000;
    if (createEmployeeDto.Salary >= BIG_SALARY)
      throw new BadRequestException("the salary is invalid!");

    var employee = new Employee
    {
      Salary = createEmployeeDto.Salary,
      ManagerId = managerId,
      StoreId = storeId,
      Password = createEmployeeDto.Password,
      Positions = []
    };

    employee.HashPassword(managerId);

    return await this._employeeRepository.AddAsync(employee);
  }
}