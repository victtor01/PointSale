using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class ProductMapper
{
  public static ProductDTO toMapper(this Product product)
  {
    return new ProductDTO
    {
      Name = product.Name,
      Price = product.Price,
      Description = product.Description ?? null,
      Quantity = product.Quantity ?? null,
    };
  }
}