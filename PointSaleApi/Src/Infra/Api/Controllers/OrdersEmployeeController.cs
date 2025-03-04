using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[IsEmployeeRoute]
[ApiController]
[Route("orders/employee")]
public class OrdersEmployeeController(IOrdersService ordersService) : OrdersControllerBase
{
  public override Task<IActionResult> Create(CreateOrderDTO createOrderDTO)
  {
    throw new NotImplementedException();
  }

  public override Task<IActionResult> FindAll()
  {
    throw new NotImplementedException();
  }

  [HttpGet]
  [PermissionsOrders(EmployeePermissionOrders.DELETE_ORDER)]
  public IActionResult Example()
  {
    SessionEmployee sessionEmployee = HttpContext.GetEmployeeSessionOrThrow();

    sessionEmployee.LoggerJson();

    return Ok("EXAMPLE");
  }
}