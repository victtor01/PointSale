using System.ComponentModel.DataAnnotations;

namespace PointSaleApi.Src.Core.Domain
{
  public abstract class User
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public abstract void HashAndSetPassword(string userId);
  }
}
