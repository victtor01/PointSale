using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Interfaces;

namespace PointSaleApi.Src.Core.Application.Services;

public class EmployeesService(
  IEmployeeRepository employeeRepository,
  IPositionsRepository positionRepository
) : IEmployeesService
{
  private readonly IPositionsRepository _positionsRepository = positionRepository;
  private readonly IEmployeeRepository _employeeRepository = employeeRepository;
  private const int COUNT_EMPLOYEE_MAX = 10;

  public async Task<List<Employee>> GetAllEmployeesAsync(Guid managerId, Guid storeId)
  {
    List<Employee> employees = await _employeeRepository.GetAllByManagerAndStoreAsync(managerId, storeId);
    return employees;
  }

  public async Task<Employee> CreateAsync(CreateEmployeeDTO createEmployeeDto, Guid managerId, Guid storeId)
  {
    List<Employee> employeesInDatabase = await this._employeeRepository
      .GetAllByManagerAndStoreAsync(managerId, storeId);

    if (employeesInDatabase.Count > COUNT_EMPLOYEE_MAX)
      throw new BadRequestException("count of employees in database exceeded");

    const int BIG_SALARY = 1000 * 1000;
    if (createEmployeeDto.Salary >= BIG_SALARY)
      throw new BadRequestException("the salary is invalid!");

    var employee = new Employee
    {
      FirstName = createEmployeeDto.FirstName,
      LastName = createEmployeeDto.LastName,
      Email = createEmployeeDto.Email,
      Phone = createEmployeeDto.Phone,
      Salary = createEmployeeDto.Salary,
      ManagerId = managerId,
      StoreId = storeId,
      Password = createEmployeeDto.Password,
      Positions = []
    };

    employee.HashPassword(managerId);

    return await this._employeeRepository.AddAsync(employee);
  }

  public async Task<Employee> UpdatePositionAsync(UpdatePositionEmployeeRecord updatePositionEmployeeRecord,
    Guid employeeId)
  {
    Employee? employee = await _employeeRepository
      .FindByIdAsync(employeeId) ?? throw new NotFoundException("employee not found!");

    List<EmployeePosition> positions = await _positionsRepository
      .FindAllByIds(updatePositionEmployeeRecord.Positions);

    employee.Positions = positions;

    var updated = await _employeeRepository.UpdateAsync(employee);

    return updated;
  }

  public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId, Guid storeId)
  {
    Employee? employee = await _employeeRepository
      .FindByIdAsync(employeeId) ?? throw new NotFoundException("employee not found!");

    if (employee?.StoreId != storeId)
      throw new BadRequestException("employee not found!");

    return employee;
  }

  public async Task<Employee> UpdateEmployee(Guid employeeId, UpdateEmployeeRecord updateEmployeeRecord, Guid managerId)
  {
    Employee? employee = await _employeeRepository.FindByIdTracking(employeeId) ?? throw new NotFoundException("employee not found!");

    employee.IsValidManager(managerId);

    List<EmployeePosition> positions = await _positionsRepository
      .FindAllByIds(updateEmployeeRecord.Positions);

    employee.FirstName = updateEmployeeRecord.FirstName;
    employee.LastName = updateEmployeeRecord.LastName;
    employee.Salary = updateEmployeeRecord.Salary;
    employee.Email = updateEmployeeRecord.Email;
    employee.Phone = updateEmployeeRecord.Phone;

    employee.Positions.Clear();
    if (positions.Any())
      employee.Positions = [.. positions];

    return await _employeeRepository.UpdateAsync(employee);
  }
}