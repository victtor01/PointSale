using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IStoresService
{
  public Task<List<Store>> GetAllByManager(Guid userId);
  public Task<Store> SaveAsync(CreateStoreDTO createStoreDto, Guid managerId);
  public Task<Store?> FindOneByIdAsync(Guid storeId);
  public Task<Store> FindOneByIdAndManagerOrThrowAsync(Guid storeId, Guid managerId);
}