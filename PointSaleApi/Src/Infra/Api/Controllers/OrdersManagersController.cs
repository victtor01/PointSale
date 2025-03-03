using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[IsAdminRoute]
[ApiController]
[Route("/orders/managers")]
public class OrdersManagersController(
  IOrdersService ordersService,
  IFindOrdersService findOrdersService,
  IOrdersCauculator ordersCauculator
) : OrdersControllerBase
{
  private readonly IOrdersService _ordersService = ordersService;
  private readonly IFindOrdersService _findOrdersService = findOrdersService;
  private readonly IOrdersCauculator _ordersCauculator = ordersCauculator;

  private SessionManager _sessionManager => HttpContext.GetManagerSessionOrThrow();

  [HttpPost]
  [IsStoreSelectedRoute]
  public override async Task<IActionResult> Create([FromBody] CreateOrderDTO createOrderDto)
  {
    Guid storeId = HttpContext.GetStoreOrThrow();
    Guid managerId = _sessionManager.UserId;

    Order order =
      await _ordersService.CreateAsync(createOrderDto, storeId: storeId, managerId: managerId);

    return Ok(order.ToMapper());
  }

  [HttpGet]
  [IsStoreSelectedRoute]
  public override async Task<IActionResult> FindAll()
  {
    Guid storeId = HttpContext.GetStoreOrThrow();
    Guid managerId = _sessionManager.UserId;

    List<Order> orders = await findOrdersService.ByManagerAndStoreAsync(managerId, storeId);

    List<OrderDTO> ordersDto = orders.Select(order => order.ToMapper()).ToList();

    return Ok(ordersDto);
  }

  [HttpGet("test")]
  public async Task Testt()
  {
    SessionManager session = HttpContext.GetManagerSessionOrThrow();
    session.LoggerJson();
  }

  [HttpGet("{orderId}")]
  public async Task<IActionResult> FindAsync(Guid orderId)
  {
    Guid managerId = _sessionManager.UserId;

    Order order = await findOrdersService.ByIdAndManagerAsync(orderId, managerId);

    float totalPrice = ordersCauculator.TotalPriceOfOrder(order);

    return Ok(new ResponseOrderDTO
    {
      Orders = order.ToMapper(),
      TotalPrice = totalPrice
    });
  }
}