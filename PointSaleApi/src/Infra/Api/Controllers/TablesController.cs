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

    [HttpPost]
    [IsAdminRoute]
    [IsStoreSelectedRoute]
    public async Task<IActionResult> SaveAsync([FromBody] CreateTableDto createTableDto)
    {
      Session context = HttpContext.GetSession();
      Guid managerId = context.UserId;
      StoreTable saved = await _tablesService.SaveAsync(createTableDto, managerId);
      return Ok(saved);
    }
  }
}
