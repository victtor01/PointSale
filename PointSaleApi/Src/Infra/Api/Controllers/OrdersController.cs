using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController(
  IOrdersService ordersService, 
  IFindOrdersService findOrdersService,
  IOrdersCauculator ordersCauculator
  ) : ControllerBase
{
  
  [IsStoreSelectedRoute]
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderDTO createOrderDto)
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid storeId = HttpContext.GetStoreOrThrow();
    Guid managerId = sessionManager.UserId;

    Order order =
      await ordersService.CreateAsync(createOrderDto, storeId: storeId, managerId: managerId);

    return Ok(order.ToMapper());
  }

  [HttpGet]
  [IsAdminRoute]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> GetAllByCreatedAt()
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid storeId = HttpContext.GetStoreOrThrow();
    Guid managerId = sessionManager.UserId;

    List<Order> orders = await findOrdersService.ByManagerAndStoreAsync(managerId, storeId);
    
    List<OrderDTO> ordersDto = orders.Select(order => order.ToMapper()).ToList();
    
    return Ok(ordersDto);
  }

  [IsAdminRoute]
  [HttpGet("{orderId}")]
  public async Task<IActionResult> FindAsync(Guid orderId)
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid managerId = sessionManager.UserId;
    
    Order order = await findOrdersService.ByIdAndManagerAsync(orderId, managerId);
    
    float totalPrice = ordersCauculator.TotalPriceOfOrder(order);

    return Ok(new ResponseOrderDTO
    {
      Orders = order.ToMapper(),
      TotalPrice = totalPrice
    });
  }
  
  [IsEmployeeRoute]
  [HttpGet("employee/{orderId}")]
  public IActionResult FindByIdAsync(Guid orderId)
  {
    SessionEmployee sessionManager = HttpContext.GetEmployeeSessionOrThrow();

    return Ok(sessionManager.Username);
  }
}