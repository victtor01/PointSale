using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.src.Core.Application.Dtos.AuthDtos
{
  public class SelectStoreDto
  {
    [Required(ErrorMessage = "store to login is required!")]
    public Guid StoreId { get; set; }

    [Required(ErrorMessage = "manager not found, this options is required!")]
    public Guid ManagerId { get; set; }

    public string Password { get; set; } = string.Empty;
  }
}
