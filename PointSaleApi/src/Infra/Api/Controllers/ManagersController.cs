using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos.AuthDtos;
using PointSaleApi.Src.Core.Application.Dtos.ManagersDtos;
using PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Core.Application.Mappers;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Attributes;
using PointSaleApi.Src.Infra.Extensions;

namespace PointSaleApi.Src.Infra.Api.Controllers
{
  [ApiController]
  [Route("managers")]
  public class ManagersController(IManagersService managersService) : ControllerBase
  {
    private readonly IManagersService _managersService = managersService;

    [HttpPost]
    [IsPublicRoute]
    public async Task<IActionResult> GetAllManagers([FromBody] CreateUserDto createUserDto)
    {
      var register = await _managersService.RegisterAsync(createUserDto);

      return Ok(register);
    }

    [IsAdminRoute()]
    [HttpGet("i")]
    public async Task<IActionResult> IManager()
    {
      Session session = HttpContext.GetSession();
      Manager informations = await this._managersService.FindByIdOrThrowAsync(session.UserId);

      return Ok(informations.ToManagerMapper());
    }
  }
}
