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
      OptionsProducts = orderProduct.OptionsProducts.Select(option => option.ToMapper()).ToList()
    };
  }
}