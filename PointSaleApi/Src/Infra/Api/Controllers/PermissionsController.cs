using Microsoft.AspNetCore.Mvc;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Infra.Attributes;

namespace PointSaleApi.Src.Infra.Api.Controllers;

[Route("permissions")]
[ApiController]
[IsAdminRoute]
public class PermissionsController(IPermissionsService permissionsService) : ControllerBase
{
  private readonly IPermissionsService _permissionsService = permissionsService;
  
  [HttpGet]
  public IActionResult GetAllPermissions()
  {
    var permissions = _permissionsService.GetAllPermissions();
    return Ok(permissions);
  }
}