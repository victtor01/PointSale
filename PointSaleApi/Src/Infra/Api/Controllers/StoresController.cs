using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("/stores")]
public class StoresController(
  IStoresService storesService,
  IOrdersCauculator ordersCauculator
) : ControllerBase
{
  private readonly IOrdersCauculator _ordersCauculator = ordersCauculator;
  private readonly IStoresService _storesService = storesService;

  [IsAdminRoute]
  [HttpPost]
  public async Task<Store> Save([FromBody] CreateStoreDTO createStoreDto)
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid managerId = sessionManager.UserId;

    var saved = await _storesService.SaveAsync(createStoreDto, managerId);

    return saved;
  }

  [IsAdminRoute]
  [HttpGet("my")]
  public async Task<IActionResult> FindMyStores()
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid managerId = sessionManager.UserId;

    List<Store> stores = await _storesService.GetAllByManagerWithRelationsAsync(managerId);
    
    var result = stores.Select(store => new
    {
      Store = store.ToStoreMapperWithResume(),
      Revenue = store.Orders
        .Where(order => order.Status == OrderStatus.PAID)
        .Sum(order => _ordersCauculator.TotalPriceOfOrder(order))
    });

    return Ok(result);
  }

  [IsAdminRoute]
  [HttpGet("{storeId}")]
  public async Task<ActionResult<StoreDTO>> FindStoreByIdAndManager(string storeId)
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid managerId = sessionManager.UserId;

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
    Guid userId = HttpContext.GetManagerSessionOrThrow().UserId;
    Guid storeId = HttpContext.GetStoreIdOrThrow();

    Store store = await _storesService.FindOneByIdWithRelations(storeId);
    store.isValidManager(userId);

    List<Order> orders = store.Orders
      .Where(order => order.Status == OrderStatus.PAID)
      .ToList();

    float totalPrice = orders
      .Sum(order => _ordersCauculator.TotalPriceOfOrder(order));

    return Ok(new
    {
      store = store.ToStoreMapperWithResume(),
      Revenue = totalPrice
    });
  }
}