using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Core.Domain;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrdersService ordersService) : ControllerBase
{
  [IsStoreSelectedRoute]
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] OrderDTO orderDto)
  {
    Session session = HttpContext.GetSession();
    Guid storeId = HttpContext.GetStoreOrthrow();
    Guid managerId = session.UserId;

    Order order =
      await ordersService.CreateAsync(orderDto, storeId: storeId, managerId: managerId, tableId: orderDto.tableId);

    return Ok(order);
  }
}