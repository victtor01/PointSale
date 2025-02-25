using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Services;

public class OrdersCauculator : IOrdersCauculator
{
  public float TotalPriceOfOrder(Order order)
  {
    List<OrderProduct>? ordersProducts = order?.OrderProducts ?? null;
    if (ordersProducts == null || ordersProducts.Count == 0) return 0;

    float totalPrice = 0;
    foreach (var currentOrder in ordersProducts)
    {
      if (currentOrder?.Product != null) totalPrice += currentOrder.Quantity * currentOrder.Product.Price;

      float priceOfOptions = currentOrder?.OptionsProducts?.Where(op => op.Product != null)
        .Select(op => op.Product!.Price)
        .Sum() ?? 0;

      totalPrice += priceOfOptions;
    }

    return totalPrice;
  }
}