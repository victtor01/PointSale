using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.src.Core.Application.Interfaces;

public interface IProductsRepository
{
  public Task<Product> SaveAsync(Product product);
  public Task<Product?> FindByIdAsync(Guid id);
  public Task<List<Product>> FindAllByStoreAndManager(Guid storeId);
}