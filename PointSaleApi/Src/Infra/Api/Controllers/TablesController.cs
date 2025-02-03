using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Core.Domain;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("tables")]
public class TablesController(ITablesService tablesService) : ControllerBase
{
  [IsAdminRoute]
  [HttpPost]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> SaveAsync([FromBody] CreateTableDTO createTableDto)
  {
    Session context = HttpContext.GetSession();
    Guid storeId = HttpContext.GetStoreOrthrow();
    Guid managerId = context.UserId;

    StoreTable saved = await tablesService.SaveAsync(
      createTableDto: createTableDto,
      managerId: managerId,
      storeId: storeId
    );

    return Ok(saved.ToMapper());
  }

  [IsAdminRoute]
  [HttpGet]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> FindByStoreSelected()
  {
    Session context = HttpContext.GetSession();
    Guid storeId = HttpContext.GetStoreOrthrow();
    Guid managerId = context.UserId;

    var tables = await tablesService.FindAllByStoreIdAndManagerOrThrowAsync(
      managerId: managerId,
      storeId: storeId
    );

    return Ok(tables.Select(table => table.ToMapper()).ToList());
  }

  [IsAdminRoute]
  [HttpGet("{idString}")]
  public async Task<IActionResult> FindByIdAndManager(string idString)
  {
    var id = Guid.TryParse(idString, out var tableId) ? tableId : throw new BadRequestException("Not a valid id");
    var context = HttpContext.GetSession();
    var managerId = context.UserId;

    var table = await tablesService.FindByIdAndManagerOrThrowAsync(
      managerId: managerId,
      tableId: id
    );

    return Ok(table.ToMapper());
  }

  [IsAdminRoute]
  [HttpDelete("{idString}")]
  public async Task<IActionResult> DeleteAsync(string idString)
  {
    var tableId = Guid.TryParse(idString, out var id) ? id : throw new BadRequestException("Not a valid id");
      
    var managerId = HttpContext.GetSession().UserId;
    var deleted = await tablesService.DeleteAsync(
      managerId:managerId, tableId: tableId);

    return Ok("deleted");
  }
}