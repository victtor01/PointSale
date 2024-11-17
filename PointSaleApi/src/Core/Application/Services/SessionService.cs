using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.SessionInterfaces;

namespace PointSaleApi.src.Core.Application.Services
{
  public class SessionService(IJwtService jwtService) : ISessionService
  {
    private readonly IJwtService _jwtService = jwtService;

    public JwtTokensDto CreateSessionUser(string userId, string email, string role)
    {
      Claim[] claims =
      [
        new("userId", userId),
        new("_email", email),
        new("_role", role),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      ];

      DateTime expiration = DateTime.UtcNow.AddMinutes(1);
      DateTime longerExpiration = DateTime.UtcNow.AddMinutes(10);

      var token = _jwtService.GenerateToken(claims, expiration);
      var refreshToken = _jwtService.GenerateToken(claims, longerExpiration);
      string tokenString = _jwtService.ParseJwtTokenToString(token);
      string refreshTokenString = _jwtService.ParseJwtTokenToString(refreshToken);

      JwtTokensDto jwtDto = new() { AccessToken = tokenString, RefreshToken = refreshTokenString };

      return jwtDto;
    }
  }
}
