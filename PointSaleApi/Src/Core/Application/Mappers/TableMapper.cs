using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class TableMapper
{
  public static TableDTO ToMapper(this StoreTable table)
  {
    return new TableDTO { Number = table.Number, Id = table.Id };
  }
}

public class TableDTO
{
  public required int Number { get; set; }
  public required Guid Id { get; set; }
}