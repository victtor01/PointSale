namespace PointSaleApi.Src.Core.Application.Dtos;

public class CreateStoreDTO
{
  public required string Name { get; set; }
  public string? Password { get; set; }
  public float? Revenue { get; set; }
}