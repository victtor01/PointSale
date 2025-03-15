using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[IsAdminRoute]
[ApiController]
[IsStoreSelectedRoute]
[Route("positions")]
public class PositionsController(IEmployeePositionsService employeePositionsService) : ControllerBase
{
  private readonly IEmployeePositionsService _employeePositionsService = employeePositionsService;

  private Guid _managerId => HttpContext.GetManagerSessionOrThrow().UserId;
  private Guid _storeId => HttpContext.GetStoreIdOrThrow();

  [HttpPost]
  public async Task<IActionResult> CreateAsync(CreateEmployeePositionDTO createEmployeePositionDto)
  {
    var created = await this._employeePositionsService
      .CreateAsync(createEmployeePositionDto, _managerId, _storeId);

    return Ok(created);
  }

  [HttpPut("{positionId}")]
  public async Task<IActionResult> UpdateAsync(Guid positionId,
    UpdateEmployeePositionRecord updateEmployeePositionRecord)
  {
    var updated = await this._employeePositionsService.UpdateAsync(
      managerId: _managerId,
      positionId: positionId,
      updateEmployeePositionRecord: updateEmployeePositionRecord
    );

    return Ok(updated);
  }

  [HttpGet("{positionId}")]
  public async Task<IActionResult> GetByIdAsync(Guid positionId)
  {
    EmployeePosition position = await this._employeePositionsService
      .GetByIdAsync(_managerId, positionId);

    return Ok(position.ToEmployeePositionDTO());
  }

  [HttpGet]
  public async Task<IActionResult> GetAllAsync()
  {
    List<EmployeePosition> positions = await _employeePositionsService
      .GetAllAsync(_managerId, _storeId);

    return Ok(positions?.Select(p => p.ToEmployeePositionDTO()).ToList());
  }
}