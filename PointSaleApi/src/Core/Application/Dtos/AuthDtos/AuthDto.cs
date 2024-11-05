using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSaleApi.src.Core.Application.Dtos.AuthDtos
{
  public class AuthDto
  {
    public required string Email { get; set; }
    public required string Password { get; set; }
  }
}
