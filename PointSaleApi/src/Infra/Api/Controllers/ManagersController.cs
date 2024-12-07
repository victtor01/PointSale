using Microsoft.AspNetCore.Mvc;
using PointSaleApi.src.Core.Application.Dtos.ManagersDtos;
using PointSaleApi.src.Core.Application.Interfaces.ManagersInterfaces;
using PointSaleApi.src.Infra.Attributes;

namespace PointSaleApi.src.Infra.Api.Controllers
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
