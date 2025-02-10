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

    return Ok(order.ToMapper());
  }

  [HttpGet]
  [IsAdminRoute]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> GetAllByCreatedAt()
  {
    Session session = HttpContext.GetSession();
    Guid storeId = HttpContext.GetStoreOrthrow();
    Guid managerId = session.UserId;

    List<Order> orders = await ordersService.GetAllAsync(managerId, storeId);
    
    List<OrderDTO> ordersDto = orders.Select(order => order.ToMapper()).ToList();
    
    return Ok(ordersDto);
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