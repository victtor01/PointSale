using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.Src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.SessionInterfaces;
using PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Application.Services;

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

  public bool VerifyPasswordOrThrowError(string userId, string password, string hash)
  {
    var Indentity = new IdentityUser { Id = userId };
    var passwordHasher = new PasswordHasher<IdentityUser>();
    var hashed = passwordHasher.VerifyHashedPassword(Indentity, hash, password);

    if (hashed == PasswordVerificationResult.Failed)
      throw new UnauthorizedException("incorrect password!");

    return true;
  }

  public async Task<JwtTokensDto> AuthManager(AuthDto authDto)
  {
    Manager manager = await _managersService.FindByEmailOrThrowAsync(authDto.Email);

    try
    {
      VerifyPasswordOrThrowError(
        password: authDto.Password,
        userId: manager.Id.ToString(),
        hash: manager.Password
      );
    }
    catch (Exception e)
    {
      Logger.Error(e.Message);
      throw new UnauthorizedException("email or password is incorrect!");
    }

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

    if (store.Password != null && string.IsNullOrEmpty(selectStoreDto.Password))
      throw new BadRequestException("This store is required password!");

    if (store.Password != null)
    {
      try
      {
        VerifyPasswordOrThrowError(
          userId: selectStoreDto.ManagerId.ToString(),
          password: selectStoreDto.Password,
          hash: store.Password
        );
      }
      catch (Exception e)
      {
        Logger.Error(e.Message);
        throw new BadRequestException("password store is incorrect!");
      }
    }

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
