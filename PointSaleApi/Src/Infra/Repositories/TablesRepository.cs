using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class TablesRepository(DatabaseContext databaseContext) : ITablesRepository
{
  private readonly DatabaseContext _context = databaseContext;

  public async Task<List<StoreTable>?> FindAllByStore(Guid storeId)
  {
    var storeTables = await _context.Tables.AsNoTracking()
      .Where(table => table.StoreId == storeId).Include(table => table.Orders).ToListAsync();

    return storeTables;
  }

  public async Task<int> DeleteAsync(Guid tableId)
  {
    var deleted = await _context.Tables.Where(t => t.Id == tableId).ExecuteDeleteAsync();
    return deleted;
  }

  public async Task<StoreTable> UpdateAsync(StoreTable table)
  {
    var updated = _context.Tables.Update(table);
    await _context.SaveChangesAsync();
    return updated.Entity;
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