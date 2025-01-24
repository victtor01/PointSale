using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointSaleApi.Src.Core.Application.Dtos.AuthDtos
{
  public class AuthStoreDto
  {
    // [Required(ErrorMessage = "password field of store is required!")]
    public string? Password { get; set; } = null;
  }
}
