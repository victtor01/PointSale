namespace PointSaleApi.Src.Core.Application.Dtos;

public class OptionsProductDTO
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public required float Price { get; set; }
  public required Guid? ProductId { get; set; }
}