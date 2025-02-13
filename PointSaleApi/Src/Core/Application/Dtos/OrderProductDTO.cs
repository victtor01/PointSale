
using PointSaleApi.Src.Core.Application.Enums;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class OrderProductDTO
{
  public required Guid Id { get; set; }
  public required int Quantity { get; set; }
  public required List<OptionsProductDTO?> Options { get; set; } = new List<OptionsProductDTO>();
  public required Guid ProductId { get; set; }
  public required Guid OrderId { get; set; }
  public OrderDTO? Order { get; set; }
  public OrderProductStatus? Status { get; set; }
  public ProductDTO? Product { get; set; }
  public DateTime? CreatedAt { get; set; } = null;
  public DateTime? UpdatedAt { get; set; } = null;
}