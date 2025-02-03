using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class CreateStoreDTO
{
  [Required(ErrorMessage = "Name of store is required!")]
  public required string Name { get; set; }
  public string? Password { get; set; }
}