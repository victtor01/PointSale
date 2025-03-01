using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[ApiController]
[Route("managers")]
public class ManagersController(IManagersService managersService) : ControllerBase
{
  private readonly IManagersService _managersService = managersService;

  [HttpPost]
  [IsPublicRoute]
  public async Task<IActionResult> GetAllManagers([FromBody] CreateUserDTO createUserDto)
  {
    Manager register = await _managersService.RegisterAsync(createUserDto);

    return Ok(register);
  }

  [IsAdminRoute()]
  [HttpGet("i")]
  public async Task<IActionResult> IManager()
  {
    SessionManager sessionManager = HttpContext.GetManagerSessionOrThrow();
    Manager informations = await this._managersService.FindByIdOrThrowAsync(sessionManager.UserId);

    return Ok(informations.ToManagerMapper());
  }
}