using PointSaleApi.Src.Core.Application.Dtos;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IAuthService
{
  public Task<JwtTokensDTO> AuthManager(AuthDTO authDto);
  public Task<string> AuthSelectStore(SelectStoreDTO selectStoreDto);
  public Task<JwtTokensDTO> AuthEmployee(AuthEmployeeDTO authDto);
}