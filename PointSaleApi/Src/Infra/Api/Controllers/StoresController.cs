using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Core.Domain;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("/stores")]
public class StoresController(IStoresService storesService, IOrdersService ordersService) : ControllerBase
{
  private readonly IStoresService _storesService = storesService;
  private readonly IOrdersService _ordersService = ordersService;

  [IsAdminRoute]
  [HttpPost()]
  public async Task<Store> Save([FromBody] CreateStoreDTO createStoreDto)
  {
    Session session = HttpContext.GetSession();
    Guid managerId = session.UserId;

    var saved = await _storesService.SaveAsync(createStoreDto, managerId);

    return saved;
  }

  [IsAdminRoute]
  [HttpGet("my")]
  public async Task<IActionResult> FindMyStores()
  {
    Session session = HttpContext.GetSession();
    Guid managerId = session.UserId;

    List<Store> stores = await _storesService.GetAllByManagerWithRelationsAsync(managerId);
    var result = stores.Select(store =>
    {
      var orders = store.Orders
        .Where(order => order.Status == OrderStatus.PAID)
        .ToList();

      float totalPrice = orders
        .Sum(order => _ordersService.GetTotalPriceOfOrder(order));

      return new
      {
        Store = store.ToStoreMapperWithResume(),
        Revenue = totalPrice
      };
    });
    
    return Ok(result);
  }

  [IsAdminRoute]
  [HttpGet("{storeId}")]
  public async Task<IActionResult> FindStoreByIdAndManager(string storeId)
  {
    Session session = HttpContext.GetSession();
    Guid managerId = session.UserId;

    Store store = await _storesService.FindOneByIdAndManagerOrThrowAsync(
      storeId: Guid.Parse(storeId),
      managerId: managerId
    );

    return Ok(store.ToStoreMapper());
  }

  [IsAdminRoute]
  [IsStoreSelectedRoute]
  [HttpGet("informations")]
  public async Task<IActionResult> GetInformations()
  {
    Guid userId = HttpContext.GetSession().UserId;
    Guid storeId = HttpContext.GetStoreOrThrow();

    Store store = await _storesService.FindOneByIdWithRelations(storeId);
    store.isValidManager(userId);

    List<Order> orders = store.Orders
      .Where(order => order.Status == OrderStatus.PAID)
      .ToList();

    float totalPrice = orders
      .Sum(order => _ordersService
        .GetTotalPriceOfOrder(order));

    return Ok(new
    {
      store = store.ToStoreMapperWithResume(),
      Revenue = totalPrice
    });
  }
}