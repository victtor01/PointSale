using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces;

public interface IProductsService
{
  public Task<Product> SaveAsync(CreateProductDTO createProductDto, Guid managerId, Guid storeId);
  public Task<List<Product>> GetAllProductsAsync(Guid storeId, Guid managerId);
  public Task<Product> FindByIdAsync(Guid productId, Guid storeId);
  public Task<Product> UpdateAsync(Guid productId, UpdateProductRecord updateProductRecord, Guid storeId);
}