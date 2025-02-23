using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[Route("employee")]
public class EmployeeController(IEmployeesService employeesService) : ControllerBase
{
  private readonly IEmployeesService _employeesService = employeesService;

  [HttpPost]
  [IsAdminRoute]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> Create([FromBody] CreateEmployeeDTO createEmployeeDto)
  {
    Guid managerId = HttpContext.GetSession().UserId;
    Guid storeId = HttpContext.GetStoreOrThrow();

    var created = await _employeesService.CreateAsync(createEmployeeDto, managerId, storeId);
    return Ok("teste");
  }
}