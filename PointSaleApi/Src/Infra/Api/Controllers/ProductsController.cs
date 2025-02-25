using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductsController(IProductsService _productsService) : ControllerBase
{
  private Guid userId => HttpContext.GetManagerSessionOrThrow().UserId;

  [HttpPost]
  [IsAdminRoute]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDto)
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid storeId = HttpContext.GetStoreOrThrow();
    Product product = await _productsService.SaveProduct(createProductDto, managerId: sessionManager.UserId, storeId: storeId);

    return Ok(product.toMapper());
  }

  [HttpGet]
  [IsStoreSelectedRoute]
  [IsAdminRoute]
  public async Task<IActionResult> GetAll()
  {
    var storeId = HttpContext.GetStoreOrThrow();

    List<Product> products = await _productsService.GetAllProducts(
      storeId: storeId, managerId: userId
    );

    return Ok(new { products = products.Select(product => product.toMapper()).ToList() });
  }
}