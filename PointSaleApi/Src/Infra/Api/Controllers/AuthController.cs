using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
  private readonly IAuthService _authService = authService;

  [HttpPost]
  [IsPublicRoute]
  public async Task<IActionResult> Auth([FromBody] AuthDTO authDto)
  {
    JwtTokensDTO logged = await _authService.AuthManager(authDto);
    
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
  
  [IsAdminRoute]
  [HttpPost("select/{storeId}")]
  public async Task<IActionResult> Select(
    [FromBody] AuthStoreDTO authStoreDto,
    [FromRoute] Guid storeId
  )
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();

    string token = await _authService.AuthSelectStore(
      new SelectStoreDTO
      {
        ManagerId = sessionManager.UserId,
        Password = authStoreDto.Password,
        StoreId = storeId,
      }
    );

    CookieOptions cookieOptions = new() { HttpOnly = true };
    HttpContext.Response.Cookies.Append("_store", token, cookieOptions);

    AuthSelectStoreDTO authSelectStoreDto = new AuthSelectStoreDTO()
    {
      Error = false,
      Message = "login selected successfully.",
    };

    return Ok(authSelectStoreDto);
  }

  [IsPublicRoute]
  [HttpPost("employee")]
  public async Task<IActionResult> AuthEmployee([FromBody] AuthEmployeeDTO authEmployeeDto)
  {
    JwtTokensDTO logged = await _authService.AuthEmployee(authEmployeeDto);
    
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
  
}