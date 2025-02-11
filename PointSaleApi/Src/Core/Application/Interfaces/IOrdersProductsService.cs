using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersProductsService
{
  public Task<OrderProduct> SaveAsync(CreateOrderProductDTO createOrderProductDto, Guid storeId);
  public Task<bool> UpdateStatusAsync(string status, Guid orderProductId);
  public Task<List<OrderProduct>> GetAllByStoreAsync(Guid storeId);
  public Task<OrderProduct> FindByIdAsync(Guid id);
}