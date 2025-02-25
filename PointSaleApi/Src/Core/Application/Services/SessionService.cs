using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Utils;

namespace PointSaleApi.Src.Core.Application.Services;

public class SessionService(IJwtService jwtService) : ISessionService
{
  private readonly IJwtService _jwtService = jwtService;

  public JwtTokensDTO CreateSessionUser(string userId, string email, string role)
  {
    Claim[] claims =
    [
      new(ClaimsKeySession.UserId, userId),
      new(ClaimsKeySession.Email, email),
      new(ClaimsKeySession.Role, role)
    ];

    return GenerateTokens(claims);
  }

  public JwtTokensDTO CreateSessionEmployee(int username, string role, Guid storeId)
  {
    Claim[] claims =
    [
      new(ClaimsKeySessionEmployee.Username, username.ToString()),
      new(ClaimsKeySessionEmployee.Store, storeId.ToString()),
      new(ClaimsKeySessionEmployee.Role, role),
    ];

    return GenerateTokens(claims);
  }

  private JwtTokensDTO GenerateTokens(Claim[] claims)
  {
    claims = claims.Append(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())).ToArray();

    DateTime expiration = DateTime.UtcNow.AddMinutes(1);
    DateTime longerExpiration = DateTime.UtcNow.AddMinutes(20);

    var token = _jwtService.GenerateToken(claims, expiration);
    var refreshToken = _jwtService.GenerateToken(claims, longerExpiration);

    return new JwtTokensDTO
    {
      AccessToken = _jwtService.ParseJwtTokenToString(token),
      RefreshToken = _jwtService.ParseJwtTokenToString(refreshToken)
    };
  }
}