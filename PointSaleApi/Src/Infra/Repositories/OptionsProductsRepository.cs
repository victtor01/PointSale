using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.Src.Infra.Repositories;

public class OptionsProductsRepository(DatabaseContext databaseContext) : IOptionsProductsRepository
{
  private readonly DatabaseContext _databaseContext = databaseContext;

  public async Task<List<OptionsProduct>> FindByIdsAsync(List<Guid> ids)
  {
    return await _databaseContext.OptionsProducts.Where(op => ids.Contains(op.Id)).ToListAsync();
  }
}