using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class StoresService(IStoresRepository _storesRepository) : IStoresService
{
  private static void IsValidStoreToCreate(List<Store> stores, string name)
  {
    if (stores.Count >= 4)
      throw new UnauthorizedException("count of stores!");

    var nameIsEqual = stores.Any(store => store.Name == name);
    if (nameIsEqual)
      throw new BadRequestException("you have a tore with that name");
  }

  public async Task<Store> FindOneByIdOrThrowAsync(Guid storeId)
  {
    return await _storesRepository.FindOneById(storeId) ?? throw new NotFoundException("store not found!");
  }

  public async Task<Store> FindOneByIdAndManagerOrThrowAsync(Guid storeId, Guid managerId)
  {
    Store? store = await _storesRepository.FindOneById(storeId);

    if (store == null || store?.ManagerId != managerId)
      throw new BadRequestException("store not found!");

    return store;
  }

  public async Task<Store> FindOneByIdWithRelations(Guid storeId)
  {
    var stores = await _storesRepository.FindByIdWithTablesWithOrdersAndProductsAsync(storeId);
    if(stores == null) throw new NotFoundException("store not found!");
    
    return stores;
  }
  
  public async Task<List<Store>> GetAllByManager(Guid managerId)
  {
    List<Store> stores = await _storesRepository.FindAllByManagerAsync(managerId);
    return stores;
  }

  public async Task<List<Store>> GetAllByManagerWithRelations(Guid managerId)
  {
    List<Store> stores = await _storesRepository.FindAllByManagerIdWithOrdersAsync(managerId);
    
    return stores;
  }

  public async Task<Store> SaveAsync(CreateStoreDTO createStoreDto, Guid managerId)
  {
    var storesOfManager = await GetAllByManager(managerId);
    IsValidStoreToCreate(stores: storesOfManager, name: createStoreDto.Name);

    var storeToSave = new Store
    {
      Name = createStoreDto.Name,
      Password = createStoreDto.Password,
      RevenueGoal = createStoreDto.Revenue,
      ManagerId = managerId,
    };

    if (!string.IsNullOrEmpty(createStoreDto.Password))
    {
      storeToSave.Password = createStoreDto.Password;
      storeToSave.HashAndSetPassword(managerId.ToString());
    }

    var saved = await _storesRepository.SaveAsync(storeToSave);

    return saved;
  }
}