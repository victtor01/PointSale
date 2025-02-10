using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class OrderProductsMapper
{
  public static OrderProductDTO ToMapper(this OrderProduct orderProduct)
  {
    return new OrderProductDTO
    {
      Quantity = orderProduct.Quantity,
      ProductId = orderProduct.ProductId,
      OrderId = orderProduct.OrderId,
      CreatedAt = orderProduct?.CreatedAt ?? null,
      UpdatedAt = orderProduct?.UpdatedAt ?? null,
      Product = orderProduct.Product.toMapper() ?? null,
      Options = orderProduct.OptionsProducts.Select(option => option.ToMapper()).ToList()
    };
  }
}