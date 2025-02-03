using PointSaleApi.Src.Core.Application.Dtos;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IAuthService
{
  public Task<JwtTokensDTO> AuthManager(AuthDto authDto);
  public Task<string> AuthSelectStore(SelectStoreDTO selectStoreDto);
}