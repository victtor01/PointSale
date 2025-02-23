using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Domain;

public abstract class BaseEntity
{
  [Required]
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  
  [Required]
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  
  public void UpdateAtToNow() => UpdatedAt = DateTime.UtcNow;
}