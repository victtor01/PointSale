using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Application.Dtos.StoresDtos
{
  public class CreateStoreDto
  {
    [Required(ErrorMessage = "Name of store is required!")]
    public required string Name { get; set; }
    public string? Password { get; set; }
  }
}
