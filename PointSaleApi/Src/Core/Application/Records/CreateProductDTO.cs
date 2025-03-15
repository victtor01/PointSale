namespace PointSaleApi.Src.Core.Application.Records;

public record CreateProductDTO (string Name, float Price, int? Quantity, string Description)
{
}