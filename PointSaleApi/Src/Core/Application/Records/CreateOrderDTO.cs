using PointSaleApi.Src.Core.Application.Enums;

namespace PointSaleApi.Src.Core.Application.Records;

public record CreateOrderDTO(Guid TableId, OrderStatus orderStatus)
{
}