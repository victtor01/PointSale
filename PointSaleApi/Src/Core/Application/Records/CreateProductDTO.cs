namespace PointSaleApi.Src.Core.Application.Records;

public record CreateProductDTO(string Name, float Price, string Description, int? Quantity)
{
}