using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSaleApi.src.Core.Application.Dtos.JwtDtos
{
  public class JwtTokensDto
  {
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
  }
}
