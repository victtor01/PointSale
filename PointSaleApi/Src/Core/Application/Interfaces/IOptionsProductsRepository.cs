using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IOptionsProductsRepository
{
  public Task<List<OptionsProduct>> FindByIdsAsync(List<Guid> ids);
}