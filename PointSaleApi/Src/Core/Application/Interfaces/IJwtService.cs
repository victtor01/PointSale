using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PointSaleApi.Src.Core.Application.Interfaces;

public interface IJwtService
{
  public string ParseJwtTokenToString(JwtSecurityToken token);
  public Dictionary<string, string> VerifyTokenAndGetClaims(string token);
  public JwtSecurityToken GenerateToken(Claim[] claim, DateTime expiration);
}