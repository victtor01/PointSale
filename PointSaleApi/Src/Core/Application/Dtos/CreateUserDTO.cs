using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class CreateUserDTO
{
  [Required]
  public required string Name { get; set; }

  [Required]
  public required string Email { get; set; }

  [Required]
  public required string Password { get; set; }
}