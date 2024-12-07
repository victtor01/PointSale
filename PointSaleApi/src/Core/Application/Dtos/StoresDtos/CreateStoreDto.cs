using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.src.Core.Application.Dtos.StoresDtos
{
  public class CreateStoreDto
  {
    [Required(ErrorMessage = "Name of store is required!")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Password of store is required!")]
    [MinLength(4, ErrorMessage = "Password must contain at least 4 characters")]
    public string Password { get; set; } = string.Empty;
  }
}
