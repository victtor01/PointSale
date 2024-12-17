using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.Src.Core.Domain;

namespace PointSaleApi.Src.Core.Application.Interfaces.AuthInterfaces
{
  public interface IAuthService
  {
    public Task<JwtTokensDto> AuthManager(AuthDto authDto);
    public Task<string> AuthSelectStore(SelectStoreDto selectStoreDto);
  }
}
