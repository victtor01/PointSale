using PointSaleApi.src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Config;

namespace PointSaleApi.src.Core.Application.Services
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

      var storeToSave = new Store { Name = createStoreDto.Name, ManagerId = managerId };
      var saved = await _storesRepository.SaveAsync(storeToSave);

      return saved;
    }
  }
}
