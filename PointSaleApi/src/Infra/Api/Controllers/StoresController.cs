using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers
{
  [ApiController()]
  [Route("/stores")]
  public class StoresController(IStoresService storesService) : ControllerBase
  {
    private readonly IStoresService _storesService = storesService;

    [IsAdminRoute()]
    [HttpPost()]
    public async Task<Store> Save([FromBody] CreateStoreDto createStoreDto)
    {
      Session session = HttpContext.GetSession();
      Guid managerId = session.UserId;

      var saved = await _storesService.SaveAsync(createStoreDto, managerId);

      return saved;
    }

    [IsAdminRoute()]
    [HttpGet("my")]
    public async Task<ActionResult<List<Store>>> FindMyStores()
    {
      Session session = HttpContext.GetSession();
      Guid managerId = session.UserId;

      List<Store> stores = await _storesService.GetAllByManager(managerId);

      return Ok(stores);
    }

    [IsAdminRoute()]
    [HttpGet("{storeId}")]
    public async Task<IActionResult> FindStoreByIdAndManager(string storeId)
    {
      Session session = HttpContext.GetSession();
      Guid managerId = session.UserId;

      Store store = await _storesService.FindOneByIdAndManagerOrThrowAsync(
        storeId: Guid.Parse(storeId),
        managerId: managerId
      );

      return Ok(store);
    }
  }
}
