using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[IsAdminRoute]
[IsStoreSelectedRoute]
[Route("employee")]
public class EmployeeController(IEmployeesService employeesService) : ControllerBase
{
  private readonly IEmployeesService _employeesService = employeesService;

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO createEmployeeDto)
  {
    Guid managerId = HttpContext.GetManagerSessionOrThrow().UserId;
    Guid storeId = HttpContext.GetStoreIdOrThrow();

    Employee created = await _employeesService.CreateAsync(createEmployeeDto, managerId, storeId);

    return Ok(created);
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    Guid managerId = HttpContext.GetManagerSessionOrThrow().UserId;
    Guid storeId = HttpContext.GetStoreIdOrThrow();

    List<Employee> employees = await _employeesService.GetAllEmployeesAsync(managerId, storeId);

    return Ok(employees);
  }

  [HttpGet("{employeeId}")]
  public async Task<IActionResult> FindAsync(Guid employeeId)
  {
    Guid managerId = HttpContext.GetManagerSessionOrThrow().UserId;
    Guid storeId = HttpContext.GetStoreIdOrThrow();

    Employee employee = await _employeesService.GetEmployeeByIdAsync(employeeId, storeId);
    employee.IsValidManager(managerId);

    return Ok(employee);
  }

  [HttpPut("{employeeId}/positions")]
  public async Task<IActionResult> Update(Guid employeeId,
    [FromBody] UpdatePositionEmployeeRecord updatePositionEmployeeRecord)
  {
    await _employeesService.UpdatePositionAsync(
      updatePositionEmployeeRecord, employeeId
    );

    return Ok("ATUALIZADO!");
  }

  [HttpPut("{employeeId}")]
  public async Task<IActionResult> Update(Guid employeeId, [FromBody] UpdateEmployeeRecord updateEmployeeRecord)
  {
    var managerId = HttpContext.GetManagerSessionOrThrow().UserId;

    var updated = await _employeesService
      .UpdateEmployee(employeeId, updateEmployeeRecord, managerId);

    return Ok(updated);
  }
}