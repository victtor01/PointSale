using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class OrderMapper
{
  public static OrderMapperDTO ToMapper(this Order order)
  {
    return new OrderMapperDTO()
    {
      Id = order.Id,
      TableId = order.TableId,
      OrderStatus = order.Status
    };
  }
}

public class OrderMapperDTO
{
  public required Guid Id { get; set; }
  public required Guid TableId { get; set; }
  public required OrderStatus OrderStatus { get; set; }
}