using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces;

public interface IProductsService
{
  public Task<Product> SaveProduct(CreateProductDTO createProductDto, Guid managerId, Guid storeId);
  public Task<List<Product>> GetAllProducts(Guid storeId, Guid managerId);
}