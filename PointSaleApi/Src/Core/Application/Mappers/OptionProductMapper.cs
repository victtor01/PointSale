using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class OptionProductMapper
{
  public static OptionsProductDTO ToMapper(this OptionsProduct options)
  {
    return new OptionsProductDTO
    {
      Id = options.Id,
      Name = options.Name,
      Price = options.Price,
      ProductId = options.ProductId,
    };
  }
}