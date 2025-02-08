using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersProductsService(IOrdersProductsRepository ordersProductsRepository, IOptionsProductsRepository optionsProductsRepository) : IOrdersProductsService
{
  private readonly IOrdersProductsRepository _ordersProductsRepository = ordersProductsRepository;
  private readonly IOptionsProductsRepository _optionsProductsRepository = optionsProductsRepository;

  public async Task<OrderProduct> Create(CreateOrderProductDTO createOrderProductDto)
  {
    List<OptionsProduct> optionsProducts =
      await _optionsProductsRepository.FindByIdsAsync(createOrderProductDto.options);
    
    var orderProduct = new OrderProduct()
    {
      ProductId = createOrderProductDto.ProductId,
      Quantity = createOrderProductDto.Quantity,
      OrderId = createOrderProductDto.OrderId,
      OptionsProducts = optionsProducts
    };
    
    OrderProduct created = await _ordersProductsRepository.AddAsync(orderProduct);

    return created;
  }
}