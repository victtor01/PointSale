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
      CreatedAt = order?.CreatedAt ?? null,
      Table = order?.Table?.ToSimpleDTO() ?? null,
      UpdatedAt = order?.UpdatedAt ?? null,
      orderProducts = order?.OrderProducts.Select(op => op.ToMapper()).ToList() ?? null
    };
  }
  
  public static OrderDTO ToSimpleMapper(this Order order)
  {
    return new OrderDTO()
    {
      Id = order.Id,
      TableId = order.TableId,
      OrderStatus = order.Status,
      CreatedAt = order?.CreatedAt ?? null,
      Table = order?.Table?.ToSimpleDTO() ?? null,
      UpdatedAt = order?.UpdatedAt ?? null,
      orderProducts = null
    };
  }
}