using PointSaleApi.src.Core.Application.Dtos.ProductsDtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.src.Core.Application.Services
{
  public class ProductsService(IProductsRepository productsRepository) : IProductsService
  {
    private readonly IProductsRepository _productsRepository = productsRepository;

    public Task<Product> SaveProduct(CreateProductDto createProductDto)
    {
      throw new NotImplementedException();
    }
  }
}
