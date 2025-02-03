using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;

namespace PointSaleApi.src.Core.Application.Interfaces;

public interface IProductsRepository
{
  public Task<Product> SaveAsync(Product product);
}