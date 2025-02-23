using FluentAssertions;
using Moq;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Application.Services;
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
    var createEmployeeDTO = new CreateEmployeeDTO(Salary: 1000 * 1000);

    var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
      () => _employeesService.CreateAsync(createEmployeeDTO, Guid.NewGuid(), Guid.NewGuid()));

    exception.Should().NotBeNull();
  }
}