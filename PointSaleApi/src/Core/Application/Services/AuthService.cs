using Microsoft.AspNetCore.Identity;
using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.JwtInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Core.Domain;
using PointSaleApi.src.Infra.Config;

namespace PointSaleApi.src.Core.Application.Services
{
  public class AuthService(IManagersService managersService, IJwtService jwtService) : IAuthService
  {
    private readonly IManagersService _managersService = managersService;
    private readonly IJwtService _jwtService = jwtService;

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

      VerifyPassword(manager.Id.ToString(), authDto.Password, manager.Password);

      JwtTokensDto tokens = _jwtService.CreateJwtToken(
        manager.Id.ToString(),
        manager.Email,
        "ADMIN"
      );

      return tokens;
    }
  }
}
