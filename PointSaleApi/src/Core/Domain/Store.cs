using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.src.Infra.Config;

namespace PointSaleApi.src.Core.Domain
{
  public class Store
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required Guid ManagerId { get; set; }

    [ForeignKey("ManagerId")]
    public Manager? Manager { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }

    public void HashAndSetPassword(string storeId)
    {
      if (Password.Length < 4)
        throw new BadRequestException("Senha curta demais");

      var passwordHasher = new PasswordHasher<string>();
      string newPassword = passwordHasher.HashPassword(storeId, Password);
      Password = newPassword;
    }
  }
}
