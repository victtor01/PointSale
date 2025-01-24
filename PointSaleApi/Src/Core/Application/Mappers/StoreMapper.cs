using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Mappers
{
  public static class StoreMapper
  {
    public static StoreDto ToStoreMapper(this Store store)
    {
      return new StoreDto
      {
        Id = store.Id,
        Name = store.Name,
        Password = string.IsNullOrEmpty(store.Password) ? null : "loked",
      };
    }
  }

  public class StoreDto
  {
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string? Password { get; set; }
  }
}
