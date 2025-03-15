using PointSaleApi.Src.Core.Application.Enums;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class OrderDTO
{
  public required Guid Id { get; set; }
  public required Guid TableId { get; set; }
  public required OrderStatus OrderStatus { get; set; }
  public List<OrderProductDTO?> OrdersProducts { get; set; }
  public DateTime? CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public TableDTO? Table { get; set; }
}
