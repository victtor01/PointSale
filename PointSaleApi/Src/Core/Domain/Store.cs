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
    public required string Name { get; set; }
    public required Guid ManagerId { get; set; }
    public required string? Password { get; set; }
    public List<StoreTable> Tables { get; set; } = [];

    [ForeignKey("ManagerId")]
    public Manager? Manager { get; set; }
    public Product[] Products { get; set; } = [];

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
