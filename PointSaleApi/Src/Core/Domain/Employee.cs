using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using PointSaleApi.Src.Core.Application.Utils;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

[Table("employees")]
public class Employee : BaseEntity
{
  [Key] [Column("id")] public Guid Id { get; set; } = Guid.NewGuid();

  [Required]
  [StringLength(100)]
  [Column("username")]
  public int Username { get; set; } = RandomId.Generate();

  [StringLength(60)]
  [Column("first_name")]
  public required string FirstName { get; set; }

  [StringLength(50)]
  [Column("last_name")]
  public string? LastName { get; set; } = null;

  [Column("email")] [MaxLength(50)] public string? Email { get; set; }

  [Required] [Column("salary")] public decimal Salary { get; set; }

  [Column("phone")] [MaxLength(30)] public string? Phone { get; set; }

  [Column("positions")] public required List<EmployeePosition> Positions { get; set; } = [];

  [Column("password")] [MaxLength(255)] public string? Password { get; set; }

  [Column("userId")] [Required] public required Guid ManagerId { get; set; }

  [Required] [Column("storeId")] public required Guid StoreId { get; set; }

  [ForeignKey(nameof(StoreId))] public Store? Store { get; set; }

  [ForeignKey(nameof(ManagerId))] public Manager? Manager { get; set; }

  [Column("birth_date")] public DateTime? BirthDate { get; set; } = null;

  public void HashPassword(Guid userId)
  {
    if (Password?.Length < 6 || Password == null)
      throw new BadRequestException("password must be at least 6 characters long.");

    var passwordHasher = new PasswordHasher<string>();
    string newPassword = passwordHasher.HashPassword(userId.ToString(), Password);
    Password = newPassword;
  }

  public bool IsValidManager(Guid managerId)
  {
    if (managerId != this.ManagerId)
      throw new BadRequestException("this employee not belong to this manager!");
    return true;
  }
}