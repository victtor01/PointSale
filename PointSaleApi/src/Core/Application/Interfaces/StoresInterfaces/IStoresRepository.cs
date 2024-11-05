using PointSaleApi.src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces
{
  public interface IStoresRepository
  {
    public Task<Store> SaveAsync(Store store);
    public Task<List<Store>> FindAllByManagerAsync(Guid managerId);
  }
}
