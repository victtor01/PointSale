using PointSaleApi.Src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces
{
  public interface IStoresService
  {
    public Task<List<Store>> GetAllByManager(Guid userId);
    public Task<Store> SaveAsync(CreateStoreDto createStoreDto, Guid managerId);
    public Task<Store?> FindOneByIdAsync(Guid storeId);
    public Task<Store> FindOneByIdAndManagerOrThrowAsync(Guid storeId, Guid managerId);
  }
}
