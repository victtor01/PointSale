using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Services;

public class ProductsService(IProductsRepository _productsRepository) : IProductsService
{
  public async Task<Product> SaveProduct(CreateProductDTO createProductDto, Guid managerId, Guid storeId)
  {
    var productToCreate = new Product
    {
      Name = createProductDto.Name,
      Price = createProductDto.Price,
      Description = createProductDto.Description ?? null,
      Quantity = createProductDto.Quantity ?? null,
      StoreId = storeId,
    };

    Product created = await _productsRepository.SaveAsync(productToCreate);

    return created;
  } 
}