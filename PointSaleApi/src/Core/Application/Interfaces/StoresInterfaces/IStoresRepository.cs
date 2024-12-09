using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces
{
  public interface IStoresRepository
  {
    public Task<Store> SaveAsync(Store store);
    public Task<List<Store>> FindAllByManagerAsync(Guid managerId);
    public Task<Store?> FindOneById(Guid storeId);
  }
}
