using PointSaleApi.src.Core.Application.Dtos.ProductsDtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Services
{
  public class ProductsService(IProductsRepository _productsRepository) : IProductsService
  {
    public async Task<Product> SaveProduct(CreateProductDto createProductDto, Guid managerId, Guid storeId)
    {
      var productToCreate = new Product
      {
        Name = createProductDto.Name,
        Price = createProductDto.Price,
        Description = createProductDto.Description ?? null,
        Quantity = createProductDto.Quantity ?? null,
        StoreId = storeId,
        ManagerId = managerId
      };

      Product created = await _productsRepository.SaveAsync(productToCreate);

      return created;
    }
  }
}