using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersProductsService
{
  public Task<OrderProduct> Create(CreateOrderProductDTO createOrderProductDto);
}