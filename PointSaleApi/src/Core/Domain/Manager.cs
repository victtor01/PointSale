using Microsoft.AspNetCore.Identity;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain
{
  public class Manager : User
  {
    public List<Store> Stores { get; set; } = [];

    public override void HashAndSetPassword(string userId)
    {
      if (Password.Length < 6)
        throw new BadRequestException("Senha curta demais");

      var passwordHasher = new PasswordHasher<string>();
      string newPassword = passwordHasher.HashPassword(userId, Password);
      Password = newPassword;
    }
  }
}
