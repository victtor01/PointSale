namespace PointSaleApi.Src.Core.Application.Dtos;

public class ProductDTO
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public required float Price { get; set; }
  public int? Quantity { get; set; } = null;
  public string? Description { get; set; } = null;
}