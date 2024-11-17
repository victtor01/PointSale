using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.SessionInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Config;

namespace PointSaleApi.src.Core.Application.Services
{
  public class AuthService(
    IManagersService managersService,
    IJwtService jwtService,
    IStoresService storesService,
    ISessionService sessionService
  ) : IAuthService
  {
    private readonly IManagersService _managersService = managersService;
    private readonly ISessionService _sessionService = sessionService;
    private readonly IJwtService _jwtService = jwtService;
    private readonly IStoresService _storesService = storesService;

    public void VerifyPassword(string userId, string password, string hash)
    {
      var Indentity = new IdentityUser { Id = userId };
      var passwordHasher = new PasswordHasher<IdentityUser>();
      var hashed = passwordHasher.VerifyHashedPassword(Indentity, hash, password);

      if (hashed == PasswordVerificationResult.Failed)
        throw new UnauthorizedException("Email ou senha incorretos!");
    }

    public async Task<JwtTokensDto> AuthManager(AuthDto authDto)
    {
      Manager manager = await _managersService.FindByEmailOrThrowAsync(authDto.Email);

      VerifyPassword(
        password: authDto.Password,
        userId: manager.Id.ToString(),
        hash: manager.Password
      );

      JwtTokensDto tokens = _sessionService.CreateSessionUser(
        role: UserRole.ADMIN.ToString(),
        userId: manager.Id.ToString(),
        email: manager.Email
      );

      return tokens;
    }

    public async Task<string> AuthSelectStore(SelectStoreDto selectStoreDto)
    {
      var store = await _storesService.FindOneByIdAsync(selectStoreDto.StoreId);

      if (store == null || store.ManagerId != selectStoreDto.ManagerId)
        throw new UnauthorizedException("store not found!");

      Claim[] claims =
      [
        new("storeId", store.Id.ToString()),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      ];

      JwtSecurityToken token = _jwtService.GenerateToken(claims, DateTime.UtcNow.AddMinutes(1));
      string tokenString = _jwtService.ParseJwtTokenToString(token);

      return tokenString;
    }
  }
}
