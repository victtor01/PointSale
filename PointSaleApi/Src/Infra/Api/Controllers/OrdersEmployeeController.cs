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
  private readonly IOrdersService _ordersService = ordersService;

  private SessionEmployee _sessionEmployee => HttpContext.GetEmployeeSessionOrThrow();

  [HttpPost]
  [PermissionsOrders(
    EmployeePermissionOrders.CREATE_ORDER,
    EmployeePermissionOrders.UPDATE_ORDER_PADDING
  )]
  public override async Task<IActionResult> Create(CreateOrderDTO createOrderDTO)
  {
    var created = await _ordersService
      .CreateAsync(createOrderDto: createOrderDTO,
        managerId: _sessionEmployee.ManagerId,
        storeId: _sessionEmployee.StoreId);

    return Ok(created);
  }

  public override Task<IActionResult> FindAll()
  {
    throw new NotImplementedException();
  }

  [HttpGet]
  [PermissionsOrders(
    EmployeePermissionOrders.CREATE_ORDER,
    EmployeePermissionOrders.UPDATE_ORDER_PADDING
  )]
  public IActionResult Example()
  {
    SessionEmployee sessionEmployee = HttpContext.GetEmployeeSessionOrThrow();

    sessionEmployee.LoggerJson();

    return Ok("EXAMPLE");
  }
}