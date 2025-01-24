using PointSaleApi.Src.Core.Application.Dtos.TablesDtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces
{
  public interface ITablesService
  {
    public Task<List<StoreTable>> FindAllByStoreIdAndManagerOrThrowAsync(
      Guid managerId,
      Guid storeId
    );

    public Task<StoreTable> SaveAsync(CreateTableDto createTableDto, Guid managerId, Guid storeId);
    public Task<StoreTable> FindByIdAndManagerOrThrowAsync(Guid tableId, Guid managerId);
    public Task<bool> DeleteAsync(Guid tableId, Guid managerId);
  }
}