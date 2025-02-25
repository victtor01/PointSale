using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

public class FindOrdersService(IOrdersRepository ordersRepository) : IFindOrdersService
{
  private readonly IOrdersRepository _ordersRepository = ordersRepository;
  
  public async Task<List<Order>> ByManagerAndStoreAsync(Guid managerId, Guid storeId)
  {
    List<Order> orders = await _ordersRepository.FindAllByCreatedDateAsync(managerId, storeId);
    orders.ForEach(order => order.OrderProducts = order.OrderProducts.OrderBy(op => op?.CreatedAt).ToList());
    
    return orders;
  }

  public async Task<Order> ByIdAndManagerAsync(Guid orderId, Guid managerId)
  {
    Order? order = await _ordersRepository.FindByIdAsync(orderId);

    if (order == null || order.ManagerId != managerId)
      throw new NotFoundException("Order not found");

    return order;
  }
}