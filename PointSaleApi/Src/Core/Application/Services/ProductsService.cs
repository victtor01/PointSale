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

  public async Task<Product> SaveAsync(CreateProductDTO createProductDto, Guid managerId, Guid storeId)
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

  public async Task<List<Product>> GetAllProductsAsync(Guid storeId, Guid managerId)
  {
    var store = await this._storesRepository.FindOneById(storeId) ?? throw new NotFoundException("store not found to get products!");

    if (store.ManagerId != managerId) throw new NotFoundException("store not found to get products!");

    List<Product> products = await _productsRepository.FindAllByStoreAndManager(storeId);
    if (products.Count == 0) return [];

    return products;
  }

  public async Task<Product> UpdateAsync(Guid productId, UpdateProductRecord updateProductRecord, Guid storeId)
  {
    Product? product = await this._productsRepository.FindByIdAsync(productId)
    ?? throw new NotFoundException("product not found!");

    if (product?.StoreId != storeId)
      throw new BadRequestException("product does not belong to this store!");

    product.Name = updateProductRecord.Name;
    product.Price = updateProductRecord.Price;
    product.Description = updateProductRecord.Description;
    product.Quantity = updateProductRecord.Quantity;

    await _productsRepository.UpdateAsync(product);

    return product;
  }

  public async Task<Product> FindByIdAsync(Guid productId, Guid storeId)
  {
    Product product = await this._productsRepository.FindByIdAsync(productId)
    ?? throw new NotFoundException("Product not found");

    if (product?.StoreId != storeId)
    {
      throw new BadRequestException("Produto pertencente a outra loja!");
    }

    return product;
  }
}