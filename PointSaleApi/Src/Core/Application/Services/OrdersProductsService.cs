using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersProductsService(
  IProductsRepository productsRepository,
  IOrdersProductsRepository ordersProductsRepository,
  IOrdersRepository ordersRepository,
  IOptionsProductsRepository optionsProductsRepository) : IOrdersProductsService
{
  private readonly IOrdersProductsRepository _ordersProductsRepository = ordersProductsRepository;
  private readonly IOptionsProductsRepository _optionsProductsRepository = optionsProductsRepository;
  private readonly IProductsRepository _productsRepository = productsRepository;
  private readonly IOrdersRepository _ordersRepository = ordersRepository;

  public async Task<OrderProduct> SaveAsync(CreateOrderProductDTO createOrderProductDto, Guid storeId)
  {
    List<OptionsProduct> optionsProducts =
      await _optionsProductsRepository.FindByIdsAsync(createOrderProductDto.Options);

    var product = await _productsRepository.FindByIdAsync(createOrderProductDto.ProductId);
    if (product == null || product.StoreId != storeId) throw new NotFoundException("Product not found");

    var order = await _ordersRepository.FindByIdAsync(createOrderProductDto.OrderId)
      ?? throw new NotFoundException("Order not found");
    order.UpdateAtToNow();

    var orderProduct = new OrderProduct()
    {
      ProductId = createOrderProductDto.ProductId,
      Quantity = createOrderProductDto.Quantity,
      OrderId = createOrderProductDto.OrderId,
      StoreId = storeId,
      OptionsProducts = optionsProducts
    };

    OrderProduct created = await _ordersProductsRepository.SaveAsync(orderProduct);

    return created;
  }

  public async Task<List<OrderProduct>> GetAllByStoreAsync(Guid storeId)
  {
    List<OrderProduct> orderProducts = await _ordersProductsRepository
      .FindByStoreWithOrderAndWithTableAsync(storeId);

    return orderProducts;
  }

  public async Task<bool> UpdateStatusAsync(string statusString, Guid orderProductId)
  {
    var orderProduct = await _ordersProductsRepository.FindByIdAsync(orderProductId)
      ?? throw new NotFoundException("OrderProduct not found");

    orderProduct.setStatusWithString(statusString);

    try
    {
      await _ordersProductsRepository.UpdateAsync(orderProduct);
      return true;
    }
    catch (Exception)
    {
      throw new BadRequestException("there was error updating the status of order product");
    }
  }

  public async Task<OrderProduct> FindByIdAsync(Guid id)
  {
    var orderProduct = await _ordersProductsRepository.FindByIdAsync(id)
      ?? throw new NotFoundException("OrderProduct not found");
    return orderProduct;
  }
}