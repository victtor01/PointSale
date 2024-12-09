using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Dtos.ManagersDtos;
using PointSaleApi.Src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.Src.Infra.Attributes;

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
  }
}
