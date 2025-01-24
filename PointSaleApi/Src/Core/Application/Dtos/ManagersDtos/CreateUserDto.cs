using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointSaleApi.Src.Core.Application.Dtos.ManagersDtos
{
  public class CreateUserDto
  {
    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
  }
}
