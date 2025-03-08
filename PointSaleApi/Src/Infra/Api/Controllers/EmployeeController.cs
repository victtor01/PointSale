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

    List<Employee> employees = await _employeesService.GetEmployeesAsync(managerId, storeId);

    return Ok(employees);
  }

  [HttpPut("{employeeId}/positions")]
  public async Task<IActionResult> Update(Guid employeeId,
  [FromBody] UpdatePositionEmployeeRecord updatePositionEmployeeRecord)
  {
    await _employeesService.UpdateAsync(
      updatePositionEmployeeRecord, employeeId
    );

    return Ok("ATUALIZADO!");
  }
}