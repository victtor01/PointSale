using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("/orders-products")]
public class OrdersProductsController(IOrdersProductsService ordersProductsService) : ControllerBase
{
  private readonly IOrdersProductsService _ordersProductsService = ordersProductsService;
  
  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderProductDTO orderProductDTO)
  {
    OrderProduct orderProduct = await _ordersProductsService.Create(orderProductDTO);
  
    return Ok(orderProduct.ToMapper());
  }
}