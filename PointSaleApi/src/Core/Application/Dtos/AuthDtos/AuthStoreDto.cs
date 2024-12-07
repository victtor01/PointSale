using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PointSaleApi.src.Core.Application.Dtos.AuthDtos
{
  public class AuthStoreDto
  {
    [Required(ErrorMessage = "password field of store is required!")]
    public required string Password { get; set; }
  }
}
