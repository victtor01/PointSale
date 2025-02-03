using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Application.Dtos;

public class SelectStoreDTO
{
  [Required(ErrorMessage = "store to login is required!")]
  public Guid StoreId { get; set; }

  [Required(ErrorMessage = "manager not found, this options is required!")]
  public Guid ManagerId { get; set; }

  public string? Password { get; set; } = string.Empty;
}