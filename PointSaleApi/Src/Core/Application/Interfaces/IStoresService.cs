using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IStoresService
{
  public Task<List<Store>> GetAllByManagerAsync(Guid userId);
  public Task<List<Store>> GetAllByManagerWithRelationsAsync(Guid managerId);
  public Task<Store> SaveAsync(CreateStoreDTO createStoreDto, Guid managerId);
  public Task<Store> FindOneByIdOrThrowAsync(Guid storeId);
  public Task<Store> FindOneByIdWithRelations(Guid storeId);
  public Task<Store> FindOneByIdAndManagerOrThrowAsync(Guid storeId, Guid managerId);
}