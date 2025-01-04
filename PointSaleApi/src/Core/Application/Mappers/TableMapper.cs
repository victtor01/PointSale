using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers
{
  public static class TableMapper
  {
    public static TableDto ToMapper(this StoreTable table)
    {
      return new TableDto { Number = table.Number, Id = table.Id };
    }
  }

  public class TableDto
  {
    public required int Number { get; set; }
    public required Guid Id { get; set; }
  }
}
