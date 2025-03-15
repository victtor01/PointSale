namespace PointSaleApi.Src.Core.Application.Records;

public record CreateOrderProductDTO(int Quantity, List<Guid> Options, Guid ProductId, Guid OrderId)
{
}