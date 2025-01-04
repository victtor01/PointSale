using PointSaleApi.src.Core.Application.Dtos.ProductsDtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Interfaces
{
  public interface IProductsService
  {
    public Task<Product> SaveProduct(CreateProductDto createProductDto, Guid managerId, Guid storeId);
  }
}
