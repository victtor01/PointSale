using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("orders/employee")]
public class OrdersEmployeeController : OrdersControllerBase
{
  public override async Task<IActionResult> Create(CreateOrderDTO createOrderDTO)
  {
    throw new NotImplementedException();
  }

  public override Task<IActionResult> FindAll()
  {
    throw new NotImplementedException();
  }

  [HttpGet]
  [IsEmployeeRoute]
  [PermissionsOrders(EmployeePermissionOrders.CREATE)]
  public IActionResult Example()
  {
    SessionEmployee sessionEmployee = HttpContext.GetEmployeeSessionOrThrow();

    sessionEmployee.LoggerJson();

    return Ok("EXAMPLE");
  }
}