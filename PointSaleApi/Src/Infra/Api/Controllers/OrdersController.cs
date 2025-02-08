using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Core.Domain;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController(IOrdersService ordersService) : ControllerBase
{
  [IsStoreSelectedRoute]
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderDTO createOrderDto)
  {
    Session session = HttpContext.GetSession();
    Guid storeId = HttpContext.GetStoreOrthrow();
    Guid managerId = session.UserId;

    Order order =
      await ordersService.CreateAsync(createOrderDto, storeId: storeId, managerId: managerId);

    return Ok(order);
  }

  [HttpGet("table/{tableId}")]
  public async Task<IActionResult> FindAllByTableIdAsync(Guid tableId)
  {
    Session session = HttpContext.GetSession();

    List<Order> orders =
      await ordersService.FindAllByTableIdAndManagerAsync(tableId: tableId, managerId: session.UserId);

    return Ok(orders.Select(orders => orders.ToMapper()));
  }

  [HttpGet("{orderId}")]
  public async Task<IActionResult> FindAsync(Guid orderId)
  {
    Session session = HttpContext.GetSession();
    Guid userId = session.UserId;
    Order order = await ordersService.FindByIdAndManagerAsync(orderId, userId);
    float totalPrice = ordersService.GetTotalPriceOfOrder(order);

    return Ok(new ResponseOrderDTO
    {
      Orders = order.ToMapper(),
      TotalPrice = totalPrice
    });
  }
}