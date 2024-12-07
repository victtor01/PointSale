using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.src.Infra.Attributes;
using PointSaleApi.src.Infra.Config;
using PointSaleApi.src.Infra.Extensions;

namespace PointSaleApi.src.Infra.Api.Controllers
{
  [ApiController]
  [Route("/auth")]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    private readonly IAuthService _authService = authService;

    public class PasswordRequest
    {
      public string Password { get; set; } = string.Empty;
    }

    [IsPublicRoute()]
    [HttpPost()]
    public async Task<IActionResult> Auth([FromBody] AuthDto authDto)
    {
      JwtTokensDto logged = await _authService.AuthManager(authDto);

      CookieOptions cookieOptions = new() { HttpOnly = true };

      HttpContext.Response.Cookies.Append(
        CookiesSessionKeys.AccessToken,
        logged.AccessToken,
        cookieOptions
      );

      HttpContext.Response.Cookies.Append(
        CookiesSessionKeys.RefreshToken,
        logged.RefreshToken,
        cookieOptions
      );

      return Ok(logged);
    }

    [IsAdminRoute()]
    [HttpPost("select/{storeId}")]
    public async Task<IActionResult> Select(
      [FromBody] AuthStoreDto authStoreDto,
      [FromRoute] Guid storeId
    )
    {
      Session session = HttpContext.GetSession();

      string token = await _authService.AuthSelectStore(
        new SelectStoreDto
        {
          ManagerId = session.UserId,
          Password = authStoreDto.Password,
          StoreId = storeId,
        }
      );

      CookieOptions cookieOptions = new() { HttpOnly = true };

      HttpContext.Response.Cookies.Append("_store", token, cookieOptions);

      return Ok("login store success");
    }
  }
}
