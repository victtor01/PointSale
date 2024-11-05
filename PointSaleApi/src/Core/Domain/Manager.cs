using Microsoft.AspNetCore.Identity;
using PointSaleApi.src.Infra.Config;

namespace PointSaleApi.src.Core.Domain
{
  public class Manager : User
  {
    public List<Store> Stores { get; set; } = [];

    public override void HashAndSetPassword(string userId, string password)
    {
      if (password.Length < 6)
        throw new BadRequestException("Senha curta demais");

      var passwordHasher = new PasswordHasher<string>();
      string newPassword = passwordHasher.HashPassword(userId, password);
      Password = newPassword;
    }
  }
}
