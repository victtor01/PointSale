using PointSaleApi.Src.Core.Application.Enums;
using PointSaleApi.Src.Core.Application.Mappers;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class OrderDTO
{
  public required Guid Id { get; set; }
  public required Guid TableId { get; set; }
  public required OrderStatus OrderStatus { get; set; }
  public required TableDTO? Table { get; set; }
}