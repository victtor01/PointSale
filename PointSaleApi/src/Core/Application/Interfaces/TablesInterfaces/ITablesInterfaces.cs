using PointSaleApi.Src.Core.Application.Dtos.TablesDtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces.TablesInterfaces;

public interface ITablesService
{
  public Task<StoreTable> SaveAsync(CreateTableDto createTableDto, Guid ManagerId);
  public Task<StoreTable> FindByNumberOrThrowAsync(int number);
}
