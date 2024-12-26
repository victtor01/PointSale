using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces.TablesInterfaces;

public interface ITablesRepository
{
  public Task<StoreTable> SaveAsync(StoreTable table);
  public Task<StoreTable?> FindByIdAsync(Guid tableId);
  public Task<StoreTable?> FindByNumberAsync(int number);
  public Task<List<StoreTable>?> FindAllByStore(Guid storeId);
}
