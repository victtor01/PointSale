using FluentAssertions;
using Moq;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace Tests.Services;

[TestClass]
public class EmployeesServiceTests
{
  private readonly Mock<IEmployeeRepository> _employeeRepository = new Mock<IEmployeeRepository>();
  private readonly EmployeesService _employeesService;

  public EmployeesServiceTests()
  {
    this._employeesService = new EmployeesService(_employeeRepository.Object);
  }

  [TestMethod]
  [Description("it should error because salary is invalid")]
  public async Task ItShouldReturnErrorBecauseSalaryIsInvalid()
  {
    var createEmployeeDTO = new CreateEmployeeDTO(Salary: 1000 * 1000, Password: "EXAMPLE");

    _employeeRepository.Setup(repo => repo.GetAllByManagerAndStoreAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
      .ReturnsAsync([]);

    var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
      () => _employeesService.CreateAsync(createEmployeeDTO, Guid.NewGuid(), Guid.NewGuid()));

    exception.Should().NotBeNull();
  }

  [TestMethod]
  public async Task ItShouldReturnErrorBecauseCountOfEmployeeIsInvalid()
  {
    const int VALID_COUNT_OF_EMPLOYEE = 10;
    const int INVALID_COUNT = 10 + VALID_COUNT_OF_EMPLOYEE;

    Guid managerId = Guid.NewGuid();
    Guid storeId = Guid.NewGuid();

    List<Employee> employees = new List<Employee>();
    for (int i = 0; i < INVALID_COUNT; i++)
    {
      employees.Add(new Employee
      {
        ManagerId = managerId,
        StoreId = storeId,
        Salary = 1000,
      });
    }

    _employeeRepository.Setup(repo => repo
        .GetAllByManagerAndStoreAsync(managerId, storeId))
      .ReturnsAsync(employees);

    decimal salaryOfEmployee = 900;
    BadRequestException exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
      () => _employeesService.CreateAsync(new CreateEmployeeDTO(Salary: salaryOfEmployee, Password: "EXAMPLE"), managerId, storeId));
    
    _employeeRepository.Verify(repo => repo.AddAsync(It.IsAny<Employee>()), Times.Never);
    
    exception.Should().NotBeNull();
    exception.Message.Should().Contain("count of employees in database exceeded");
  }
}