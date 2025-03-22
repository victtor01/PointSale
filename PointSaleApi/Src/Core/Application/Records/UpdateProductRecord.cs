namespace PointSaleApi.Src.Core.Application.Records;

public record UpdateProductRecord(
  string Name,
  float Price,
  string? Description,
  int Quantity
)
{ }