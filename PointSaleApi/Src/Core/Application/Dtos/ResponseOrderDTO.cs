using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class ResponseOrderDTO
{
  public OrderDTO? Orders { get; set; }
  public float TotalPrice { get; set; }
}