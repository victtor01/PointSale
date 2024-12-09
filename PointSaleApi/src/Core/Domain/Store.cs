using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain
{
  public class Store
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public required string Name { get; set; }

    [Required]
    public required Guid ManagerId { get; set; }

    [ForeignKey("ManagerId")]
    public Manager? Manager { get; set; }

    public string? Password { get; set; }

    public void HashAndSetPassword(string storeId)
    {
      if (Password != null && Password.Length < 4)
        throw new BadRequestException("Senha curta demais");

      if (Password != null)
      {
        var passwordHasher = new PasswordHasher<string>();
        string newPassword = passwordHasher.HashPassword(storeId, Password);
        Password = newPassword;
      }
    }
  }
}
