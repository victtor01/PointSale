using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.src.Infra.Repositories
{
  public class ProductsRepository(DatabaseContext databaseContext) : IProductsRepository
  {
    private readonly DatabaseContext _databaseContext = databaseContext;

    public async Task<Product> SaveAsync(Product product)
    {
      var saved = await this._databaseContext.Products.AddAsync(product);
      await _databaseContext.SaveChangesAsync();
      return saved.Entity;
    }
  }
}
