namespace PointSaleApi.src.Core.Application.Dtos.ProductsDtos
{
  public class CreateProductDto
  {
    public required string Name { get; set; }
    public required float Price { get; set; }
    public int? Quantity { get; set; } = null;
    public string? Description { get; set; } = null;
  }
}