using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.TablesDtos;
using PointSaleApi.src.Core.Application.Interfaces.TablesInterfaces;
using PointSaleApi.src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.src.Infra.Api.Controllers
{
  [ApiController]
  [Route("tables")]
  public class TablesController(ITablesService tablesService) : ControllerBase
  {
    private readonly ITablesService _tablesService = tablesService;

    [IsAdminRoute]
    [HttpPost]
    [IsStoreSelectedRoute]
    public async Task<IActionResult> SaveAsync([FromBody] CreateTableDto createTableDto)
    {
      Session context = HttpContext.GetSession();
      Guid storeId = HttpContext.GetStoreOrthrow();
      Guid managerId = context.UserId;

      StoreTable saved = await _tablesService.SaveAsync(
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

      var tables = await _tablesService.FindAllByStoreIdAndManagerOrThrowAsync(
        managerId: managerId,
        storeId: storeId
      );

      return Ok(tables.Select(table => table.ToMapper()).ToList());
    }
  }
}
