using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Domain;

public abstract class User
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [MaxLength(100)]
  [Required]
  public required string Name { get; set; }

  [MaxLength(100)]
  public required string Email { get; set; }
  
  [MaxLength(100)]
  public required string Password { get; set; }
  
  public abstract void HashAndSetPassword(string userId);
}