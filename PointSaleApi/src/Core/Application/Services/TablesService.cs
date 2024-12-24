using PointSaleApi.Src.Core.Application.Dtos.TablesDtos;
using PointSaleApi.src.Core.Application.Interfaces.TablesInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.TablesInterfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.src.Core.Application.Services
{
  public class TablesService(ITablesRepository tablesRepository) : ITablesService
  {
    private readonly ITablesRepository _tablesRepository = tablesRepository;

    public async Task<StoreTable> FindByNumberOrThrowAsync(int number)
    {
      var table =
        await _tablesRepository.FindByNumberAsync(number)
        ?? throw new BadRequestException("Mesa não existe!");
      return table;
    }

    private async Task<StoreTable?> FindByNumberAsync(int number)
    {
      StoreTable? table = await _tablesRepository.FindByNumberAsync(number);
      return table;
    }

    public async Task<StoreTable> SaveAsync(CreateTableDto createTableDto, Guid ManagerId)
    {
      int number = createTableDto.Number;

      if (number > 1000)
        throw new BadRequestException("Número da mesa alto demais!");

      var table = await this.FindByNumberAsync(number);
      if (table != null)
        throw new BadRequestException("Mesa já existente.");

      var tableToCreate = new StoreTable() { Number = number, ManagerId = ManagerId };
      var created = await _tablesRepository.SaveAsync(tableToCreate);

      return created;
    }
  }
}
