using PointSaleApi.Src.Core.Application.Dtos.TablesDtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services
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

    public async Task<StoreTable> SaveAsync(
      CreateTableDto createTableDto,
      Guid ManagerId,
      Guid storeId
    )
    {
      int number = createTableDto.Number;

      if (number > 1000)
        throw new BadRequestException("Número da mesa alto demais!");

      var table = await this.FindByNumberAsync(number);
      if (table != null)
        throw new BadRequestException("Mesa já existente.");

      StoreTable tableToCreate =
        new()
        {
          Number = number,
          ManagerId = ManagerId,
          StoreId = storeId,
        };

      var created = await _tablesRepository.SaveAsync(tableToCreate);

      return created;
    }

    public async Task<List<StoreTable>> FindAllByStoreIdAndManagerOrThrowAsync(
      Guid managerId,
      Guid storeId
    )
    {
      List<StoreTable> tables =
        await this._tablesRepository.FindAllByStore(storeId)
        ?? throw new NotFoundException("loja não encontrada.");

      foreach (StoreTable table in tables)
      {
        Logger.Error(table.Number.ToString());
        if (table.ManagerId != managerId)
          throw new BadRequestException("Algo deu errado ao tentar pegar as mesas da sua loja!");
      }

      return tables;
    }
  }
}
