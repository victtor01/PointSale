using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersService
{
  public Task<Order> CreateAsync(OrderDTO orderDto, Guid managerId, Guid storeId, string tableId);
}