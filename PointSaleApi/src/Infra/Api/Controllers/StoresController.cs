using Microsoft.AspNetCore.Mvc;
using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Attributes;
using PointSaleApi.src.Infra.Extensions;

namespace PointSaleApi.src.Infra.Api.Controllers
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
    public async Task<List<Store>> FindMyStores()
    {
      Session session = HttpContext.GetSession();
      Guid managerId = session.UserId;

      List<Store> stores = await _storesService.GetAllByManager(managerId);

      return stores;
    }
  }
}