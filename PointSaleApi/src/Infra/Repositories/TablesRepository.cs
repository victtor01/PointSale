using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class TablesRepository(DatabaseContext databaseContext) : ITablesRepository
{
  private readonly DatabaseContext _context = databaseContext;

  public async Task<List<StoreTable>?> FindAllByStore(Guid storeId)
  {
    var storeTables = await _context.Tables.Where(table => table.StoreId == storeId).ToListAsync();
    return storeTables;
  }

  public async Task<StoreTable?> FindByIdAsync(Guid tableId)
  {
    var storeTable =
      await _context.Tables.FirstOrDefaultAsync(table => table.Id == tableId) ?? null;

    return storeTable;
  }

  public async Task<StoreTable?> FindByNumberAsync(int number)
  {
    var storeTable =
      await _context.Tables.FirstOrDefaultAsync(table => table.Number == number) ?? null;

    return storeTable;
  }

  public async Task<StoreTable> SaveAsync(StoreTable storeTable)
  {
    var saved = await _context.Tables.AddAsync(storeTable);
    await _context.SaveChangesAsync();
    return saved.Entity;
  }
}
