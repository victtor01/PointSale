using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class StoreMapper
{
  public static StoreDTO ToStoreMapper(this Store store)
  {
    List<TableDTO> tables = store?.Tables.Count > 0
      ? store.Tables.Select(table => table.ToSimpleDTO()).ToList()
      : [];

    return new StoreDTO
    {
      Id = store.Id,
      Name = store.Name,
      Password = string.IsNullOrEmpty(store.Password) ? null : "loked",
      RevenueGoal = store.RevenueGoal ?? null,
      Tables = tables
    };
  }

  public static StoreResumeDTO ToStoreMapperWithResume(this Store store)
  {
    return new StoreResumeDTO()
    {
      Id = store.Id,
      Name = store.Name,
      Password = string.IsNullOrEmpty(store.Password) ? null : "loked",
      QuantityOfTables = store.Tables.Count,
      RevenueGoal = store.RevenueGoal
    };
  }
}

public class StoreResumeDTO
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public required string? Password { get; set; }
  public required float? RevenueGoal { get; set; }
  public int QuantityOfTables { get; set; }
}

public class StoreDTO
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public required string? Password { get; set; }
  
  public required float? RevenueGoal { get; set; }
  public List<TableDTO>? Tables { get; set; }
}