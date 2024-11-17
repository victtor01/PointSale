using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;

namespace PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces
{
  public interface IAuthService
  {
    public Task<JwtTokensDto> AuthManager(AuthDto authDto);
    public Task<string> AuthSelectStore(SelectStoreDto selectStoreDto);
  }
}
