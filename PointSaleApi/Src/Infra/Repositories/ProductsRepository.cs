using Microsoft.EntityFrameworkCore;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.src.Infra.Repositories;

public class ProductsRepository(DatabaseContext databaseContext) : IProductsRepository
{
  private readonly DatabaseContext _databaseContext = databaseContext;

  public async Task<Product> SaveAsync(Product product)
  {
    var saved = await this._databaseContext.Products.AddAsync(product);
    await _databaseContext.SaveChangesAsync();
    return saved.Entity;
  }

  public async Task<Product?> FindByIdAsync(Guid id)
  {
    var product = await this._databaseContext.Products.AsNoTracking()
      .FirstOrDefaultAsync(product => product.Id == id) ?? null;
    
    return product;
  }

  public async Task<List<Product>> FindAllByStoreAndManager(Guid storeId)
  {
    var products = await this._databaseContext.Products.AsQueryable()
      .Where(product => product.StoreId == storeId)
      .ToListAsync();

    return products;
  }
}