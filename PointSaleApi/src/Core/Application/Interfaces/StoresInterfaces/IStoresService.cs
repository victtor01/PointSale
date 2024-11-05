using PointSaleApi.src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces
{
  public interface IStoresService
  {
    public Task<List<Store>> GetAllByManager(Guid userId);
    public Task<Store> SaveAsync(CreateStoreDto createStoreDto, Guid managerId);
  }
}
