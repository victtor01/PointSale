using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers;

public static class StoreMapper
{
  public static StoreDto ToStoreMapper(this Store store)
  {
    List<TableDTO> tables = store?.Tables.Count > 0
      ? store.Tables.Select(table => table.ToSimpleDTO()).ToList()
      : [];

    return new StoreDto
    {
      Id = store.Id,
      Name = store.Name,
      Password = string.IsNullOrEmpty(store.Password) ? null : "loked",
      Tables = tables
    };
  }

  public static StoreResumeDto ToStoreMapperWithResume(this Store store)
  {
    return new StoreResumeDto()
    {
      Id = store.Id,
      Name = store.Name,
      Password = string.IsNullOrEmpty(store.Password) ? null : "loked",
      QuantityOfTables = store.Tables.Count,
    };
  }
}

public class StoreResumeDto
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public required string? Password { get; set; }
  public int QuantityOfTables { get; set; }
}

public class StoreDto
{
  public required Guid Id { get; set; }
  public required string Name { get; set; }
  public required string? Password { get; set; }

  public List<TableDTO>? Tables { get; set; }
}