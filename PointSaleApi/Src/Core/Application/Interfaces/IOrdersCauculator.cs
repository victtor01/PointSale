using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOrdersCauculator
{
  public float TotalPriceOfOrder(Order order);
}