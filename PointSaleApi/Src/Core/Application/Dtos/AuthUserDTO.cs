using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class AuthDTO
{
  [Required(ErrorMessage = "email is required")]
  public string Email { get; set; } = string.Empty;

  [Required(ErrorMessage = "password is required")]
  public string Password { get; set; } = string.Empty;
}