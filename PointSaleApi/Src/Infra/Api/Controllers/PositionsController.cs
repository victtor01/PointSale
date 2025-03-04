using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("positions")]
public class PositionsController(IEmployeePositionsService employeePositionsService) : ControllerBase
{
  private readonly IEmployeePositionsService _employeePositionsService = employeePositionsService;
  
  [IsAdminRoute]
  [HttpPost]
  public async Task<IActionResult> CreateAsync(CreateEmployeePositionDTO createEmployeePositionDto)
  {
    Guid ManagerId = HttpContext.GetManagerSessionOrThrow().UserId;
    var created = await _employeePositionsService.CreateAsync(createEmployeePositionDto, ManagerId);

    return Ok(created);
  }
}