using PointSaleApi.Src.Core.Application.Enums;

namespace PointSaleApi.Src.Core.Application.Dtos;

public record OrderDTO(string tableId, OrderStatus? OrderStatus = OrderStatus.CURRENT);