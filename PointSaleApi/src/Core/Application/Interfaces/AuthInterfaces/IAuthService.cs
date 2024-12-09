using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.JwtDtos;

namespace PointSaleApi.Src.Core.Application.Interfaces.AuthInterfaces
{
  public interface IAuthService
  {
    public Task<JwtTokensDto> AuthManager(AuthDto authDto);
    public Task<string> AuthSelectStore(SelectStoreDto selectStoreDto);
  }
}
