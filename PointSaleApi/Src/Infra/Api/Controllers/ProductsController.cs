using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Core.Application.Services;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[IsAdminRoute]
[IsStoreSelectedRoute]
[Route("products")]
public class ProductsController(IProductsService _productsService) : ControllerBase
{
  private Guid userId => HttpContext.GetManagerSessionOrThrow().UserId;

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDto)
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Guid storeId = HttpContext.GetStoreIdOrThrow();
    Product product = await _productsService.SaveAsync(createProductDto, managerId: sessionManager.UserId, storeId: storeId);

    return Ok(product.toMapper());
  }

  [HttpGet("{productId}")]
  public async Task<IActionResult> FindByIdAsync(Guid productId)
  {
    Guid storeId = HttpContext.GetStoreIdOrThrow();

    Product product = await _productsService.FindByIdAsync(productId, storeId);

    return Ok(product.toMapper());
  }

  [HttpPut("{productId}")]
  public async Task<IActionResult> UpdateAsync(Guid productId, [FromBody] UpdateProductRecord updateProductRecord)
  {
    Guid storeId = HttpContext.GetStoreIdOrThrow();

    Product product = await _productsService
    .UpdateAsync(productId, updateProductRecord, storeId);

    return Ok(product.toMapper());
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    var storeId = HttpContext.GetStoreIdOrThrow();

    List<Product> products = await _productsService.GetAllProductsAsync(
      storeId: storeId, managerId: userId
    );

    return Ok(products.Select(product => product.toMapper()).ToList());
  }
}