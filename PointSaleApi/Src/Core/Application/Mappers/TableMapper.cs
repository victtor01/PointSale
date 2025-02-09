using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class TableMapper
{
  public static TableDTO ToMapper(this StoreTable table)
  {
    return new TableDTO
    {
      Number = table.Number, Id = table.Id,
      orders = table?.Orders?.Select(order => (OrderDTO?) order.ToMapper()).ToList() 
               ?? new List<OrderDTO?>()
    };
    
  }
}