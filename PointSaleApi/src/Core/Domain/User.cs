using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.src.Core.Domain
{
  public abstract class User
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Email { get; set; }

    [Required]
    public string Password { get; set; } = string.Empty;

    public abstract void HashAndSetPassword(string userId, string password);
  }
}
