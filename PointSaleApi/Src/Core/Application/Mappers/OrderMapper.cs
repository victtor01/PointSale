using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class OrderMapper
{
  public static OrderDTO ToMapper(this Order order)
  {
    return new OrderDTO()
    {
      Id = order.Id,
      TableId = order.TableId,
      OrderStatus = order.Status,
      Table = order.Table.ToMapper()
    };
  }
  
}