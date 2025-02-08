using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class CreateOrderProductDTO
{
  public required int Quantity { get; set; }
  public required List<Guid> options { get; set; }
  public required Guid ProductId { get; set; }
  public required Guid OrderId { get; set; }
}