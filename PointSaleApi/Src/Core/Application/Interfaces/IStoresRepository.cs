using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IStoresRepository
{
  public Task<Store> SaveAsync(Store store);
  public Task<List<Store>> FindAllByManagerAsync(Guid managerId);
  public Task<Store?> FindOneById(Guid storeId);
  public Task<List<Store>> FindAllByManagerIdWithOrdersAsync(Guid managerId);
  public Task<Store?> FindByIdWithTablesWithOrdersAndProductsAsync(Guid storeId);
}