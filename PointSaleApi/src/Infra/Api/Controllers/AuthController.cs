using Microsoft.AspNetCore.Mvc;
using PointSaleApi.src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.src.Core.Application.Dtos.JwtDtos;
using PointSaleApi.src.Core.Application.Interfaces.AuthInterfaces;
using PointSaleApi.src.Infra.Attributes;
using PointSaleApi.src.Infra.Config;

namespace PointSaleApi.src.Infra.Api.Controllers
{
  [ApiController]
  [Route("/auth")]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    private readonly IAuthService _authService = authService;

    [IsPublicRoute()]
    [HttpPost()]
    public async Task<IActionResult> Auth([FromBody] AuthDto authDto)
    {
      JwtTokensDto logged = await _authService.AuthManager(authDto);

      CookieOptions cookieOptions = new() { HttpOnly = true };

      HttpContext.Response.Cookies.Append(
        CookiesNames.AccessToken,
        logged.AccessToken,
        cookieOptions
      );

      HttpContext.Response.Cookies.Append(
        CookiesNames.RefreshToken,
        logged.RefreshToken,
        cookieOptions
      );

      HttpContext.Response.Cookies.Append("Role", logged.Role, cookieOptions);

      return Ok(logged);
    }
  }
}
