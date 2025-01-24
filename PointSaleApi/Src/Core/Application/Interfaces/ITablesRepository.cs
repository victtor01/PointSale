using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface ITablesRepository
{
  public Task<StoreTable> SaveAsync(StoreTable table);
  public Task<StoreTable?> FindByIdAsync(Guid tableId);
  public Task<StoreTable?> FindByNumberAsync(int number);
  public Task<List<StoreTable>?> FindAllByStore(Guid storeId);
  public Task<StoreTable> UpdateAsync(StoreTable table);
  public Task<int> DeleteAsync(Guid tableId);
}
