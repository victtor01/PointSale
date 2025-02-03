using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

[Table("managers")]
public class Manager : User
{
  public List<Store> Stores { get; init; } = [];
    
  public override void HashAndSetPassword(string userId)
  {
    if (Password.Length < 6)
      throw new BadRequestException("Senha curta demais");

    var passwordHasher = new PasswordHasher<string>();
    var newPassword = passwordHasher.HashPassword(userId, Password);
    Password = newPassword;
  }
}