using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

[Table("stores")]
public class Store
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Column("name")]
  [Required]  
  [StringLength(100)]
  public required string Name { get; set; }
  
  [Column("description")]
  [Required]
  public required Guid ManagerId { get; set; }

  [Column("password")] 
  [StringLength(100)]
  public string? Password { get; set; } = null;

  [Column("revenue")] 
  public float? RevenueGoal { get; set; } = null;

  [ForeignKey("ManagerId")]
  public Manager? Manager { get; set; }

  public List<StoreTable> Tables { get; set; } = [];

  public List<Product> Products { get; set; } = [];
  
  public List<Order> Orders { get; set; } = [];

  public void HashAndSetPassword(string storeId)
  {
    if (Password == null || Password.Length < 4)
      throw new BadRequestException("Senha curta demais");

    var passwordHasher = new PasswordHasher<string>();
    var newPassword = passwordHasher.HashPassword(storeId, Password);
    Password = newPassword;
  }

  public void isValidManager(Guid managerId)
  {
    if (ManagerId != managerId)
    {
      throw new UnauthorizedException("manager not have this store!");
    }
  }
}