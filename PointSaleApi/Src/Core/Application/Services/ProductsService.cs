using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class ProductsService(IProductsRepository productsRepository, IStoresRepository storesRepo) : IProductsService
{
  private readonly IStoresRepository _storesRepository = storesRepo;
  private readonly IProductsRepository _productsRepository = productsRepository;

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

  public async Task<List<Product>> GetAllProducts(Guid storeId, Guid managerId)
  {
    var store = await this._storesRepository.FindOneById(storeId) ?? throw new NotFoundException("store not found to get products!");
    if (store.ManagerId != managerId) throw new NotFoundException("store not found to get products!");

    List<Product> products = await _productsRepository.FindAllByStoreAndManager(storeId);
    if (products.Count == 0) return [];

    return products;
  }
}