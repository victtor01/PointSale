using System.IdentityModel.Tokens.Jwt;
using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;

namespace PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces
{
  public interface IJwtService
  {
    public JwtTokensDto CreateJwtToken(string userId, string email, string role);
    public Dictionary<string, string> DecodeTokenAndGetClaims(string token);
    public Dictionary<string, string> GetClaimsOfJwtSecurityToken(
      JwtSecurityToken jwtSecurityToken
    );
    public JwtSecurityToken DecodeJwt(string token);
  }
}
