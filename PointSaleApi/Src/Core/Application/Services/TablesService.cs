using PointSaleApi.Src.Core.Application.Dtos.TablesDtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services
{
  public class TablesService(ITablesRepository _tablesRepository) : ITablesService
  {
    private async Task<StoreTable> FindStoreTableByIdOrThrowAsync(Guid tableId)
    {
      var table = await _tablesRepository.FindByIdAsync(tableId);
      if (table == null) throw new NotFoundException("table not found!");
      return table;
    }
    private async Task<StoreTable> FindByNumberOrThrowAsync(int number)
    {
      var table =
        await _tablesRepository.FindByNumberAsync(number)
        ?? throw new BadRequestException("table not found!");
      return table;
    }

    public async Task<bool> DeleteAsync(Guid tableId, Guid managerId)
    {
      var table = await this.FindStoreTableByIdOrThrowAsync(tableId);
      if (table.ManagerId != managerId) throw new UnauthorizedException("dont permit delete this product!");
      
      var deleted = await _tablesRepository.DeleteAsync(tableId);
      if(deleted == 0) throw new NotFoundException("table not found to delete!"); 
      
      return true;
    }

    public async Task<StoreTable> FindByIdAndManagerOrThrowAsync(Guid tableId, Guid managerId)
    {
      var table = await FindStoreTableByIdOrThrowAsync(tableId);
      
      if (table.ManagerId != managerId)
        throw new BadRequestException("Mesa not exists!");
      
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

      var table = await _tablesRepository.FindByNumberAsync(number);
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
        await _tablesRepository.FindAllByStore(storeId)
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
