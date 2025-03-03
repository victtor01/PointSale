using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[Route("employee")]
[ApiController]
[IsAdminRoute]
public class EmployeeController(IEmployeesService employeesService) : ControllerBase
{
  private readonly IEmployeesService _employeesService = employeesService;

  [HttpPost]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO createEmployeeDto)
  {
    Guid managerId = HttpContext.GetManagerSessionOrThrow().UserId;
    Guid storeId = HttpContext.GetStoreOrThrow();

    Employee created = await _employeesService.CreateAsync(createEmployeeDto, managerId, storeId);

    return Ok(created);
  }
}