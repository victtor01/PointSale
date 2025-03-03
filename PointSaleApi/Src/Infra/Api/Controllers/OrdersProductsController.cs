using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[IsAdminRoute]
[Route("/orders-products")]
public class OrdersProductsController(IOrdersProductsService ordersProductsService) : ControllerBase
{
  private readonly IOrdersProductsService _ordersProductsService = ordersProductsService;

  [HttpPost]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> Create([FromBody] CreateOrderProductDTO orderProductDTO)
  {
    Guid storeId = HttpContext.GetStoreOrThrow();
    OrderProduct orderProduct = await _ordersProductsService.SaveAsync(orderProductDTO, storeId);

    return Ok(orderProduct.ToMapper());
  }

  [HttpPut("status/{id}")]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> UpdateStatus(string id, [FromBody] UpdateStatusOrderProductDTO updateDTO)
  {
    Guid storeId = HttpContext.GetStoreOrThrow();
    Guid orderProductId = id.ToGuidOrThrow();

    OrderProduct orderProduct = await _ordersProductsService.FindByIdAsync(orderProductId);
    orderProduct.IsValidStoreId(storeId);

    await _ordersProductsService.UpdateStatusAsync(
      status: updateDTO.status, orderProductId: orderProductId);

    return Ok(new { error = false, message = "updated" });
  }

  [HttpGet]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> GetAll()
  {
    Guid storeId = HttpContext.GetStoreOrThrow();

    List<OrderProduct> orders = await this._ordersProductsService.GetAllByStoreAsync(storeId);
    List<OrderProductDTO> orderProductDTOs = orders.Select(o => o.ToMapper()).ToList();

    return Ok(new
    {
      orders = orderProductDTOs
    });
  }
}