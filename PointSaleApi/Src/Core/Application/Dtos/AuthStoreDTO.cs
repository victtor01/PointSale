namespace PointSaleApi.Src.Core.Application.Dtos;

public class AuthStoreDTO
{
  // [Required(ErrorMessage = "password field of store is required!")]
  public string? Password { get; set; } = null;
}