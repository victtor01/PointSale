namespace PointSaleApi.Src.Core.Application.Dtos;

public class OrderProductDTO
{
  public required int Quantity { get; set; }
  public required List<OptionsProductDTO> OptionsProducts { get; set; }
  public required Guid ProductId { get; set; }
  public required Guid OrderId { get; set; }
}