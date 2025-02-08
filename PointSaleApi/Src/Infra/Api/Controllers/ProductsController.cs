using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Core.Domain;
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
  [HttpPost]
  [IsAdminRoute]
  [IsStoreSelectedRoute]
  public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDto)
  {
    Session session = HttpContext.GetSession();
    Guid storeId = HttpContext.GetStoreOrthrow();
    Product product = await _productsService.SaveProduct(createProductDto, managerId: session.UserId, storeId: storeId);
    
    return Ok(product.toMapper());
  }
}