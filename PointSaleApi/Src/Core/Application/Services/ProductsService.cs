using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Services;

public class ProductsService(IProductsRepository _productsRepository) : IProductsService
{
  public async Task<Product> SaveProduct(ProductDTO productDto, Guid managerId, Guid storeId)
  {
    var productToCreate = new Product
    {
      Name = productDto.Name,
      Price = productDto.Price,
      Description = productDto.Description ?? null,
      Quantity = productDto.Quantity ?? null,
      StoreId = storeId,
    };

    Product created = await _productsRepository.SaveAsync(productToCreate);

    return created;
  }
}