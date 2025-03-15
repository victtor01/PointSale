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

  private SessionEmployee _sessionEmployee =>
    HttpContext.GetEmployeeSessionOrThrow();

  [HttpPost]
  [PermissionsOrders(EmployeePermissionOrders.CREATE_ORDER)]
  public override async Task<IActionResult> CreateAsync(CreateOrderDTO createOrderDTO)
  {
    var order = await _ordersService
      .CreateAsync(createOrderDto: createOrderDTO,
        managerId: _sessionEmployee.ManagerId,
        storeId: _sessionEmployee.StoreId);

    return CreatedAtAction(nameof(CreateAsync), new { id = order.Id }, order);
  }

  public override Task<IActionResult> FindAll()
  {
    throw new NotImplementedException();
  }
}