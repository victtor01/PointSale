using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class OrderProductsMapper
{
  public static OrderProductDTO ToMapper(this OrderProduct orderProduct)
  {
    return new OrderProductDTO
    {
      Id = orderProduct.Id,
      Quantity = orderProduct.Quantity,
      ProductId = orderProduct.ProductId,
      OrderId = orderProduct.OrderId,
      Status = orderProduct?.Status ?? null,
      CreatedAt = orderProduct?.CreatedAt ?? null,
      UpdatedAt = orderProduct?.UpdatedAt ?? null,
      Product = orderProduct?.Product?.toMapper() ?? null,
      Options = orderProduct?.OptionsProducts != null
        ? orderProduct.OptionsProducts.Select(option => (OptionsProductDTO?)option.ToMapper()).ToList()
        : []
    };
  }
}