using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;

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
}