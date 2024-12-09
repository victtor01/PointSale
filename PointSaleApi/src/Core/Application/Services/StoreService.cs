using PointSaleApi.Src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services
{
  public class StoresService(IStoresRepository storesRepository) : IStoresService
  {
    private readonly IStoresRepository _storesRepository = storesRepository;

    public async Task<Store?> FindOneByIdAsync(Guid storeId)
    {
      var store = await _storesRepository.FindOneById(storeId);
      return store;
    }

    public async Task<List<Store>> GetAllByManager(Guid managerId)
    {
      List<Store> stores = await _storesRepository.FindAllByManagerAsync(managerId);
      return stores;
    }

    public async Task<Store> SaveAsync(CreateStoreDto createStoreDto, Guid managerId)
    {
      var storesOfManager = await GetAllByManager(managerId);

      if (storesOfManager.Count >= 4)
      {
        throw new UnauthorizedException("count of stores!");
      }

      var nameIsEqual = storesOfManager.Any(store => store.Name == createStoreDto.Name);
      if (nameIsEqual)
      {
        throw new BadRequestException("you have a store with that name");
      }

      var storeToSave = new Store { Name = createStoreDto.Name, ManagerId = managerId };

      if (!string.IsNullOrEmpty(createStoreDto.Password))
      {
        storeToSave.Password = createStoreDto.Password;
        storeToSave.HashAndSetPassword(managerId.ToString());
      }

      var saved = await _storesRepository.SaveAsync(storeToSave);

      return saved;
    }
  }
}
