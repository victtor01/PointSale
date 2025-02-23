using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointSaleApi.Src.Core.Application.Utils;

namespace PointSaleApi.Src.Core.Domain;

[Table("employees")]
public class Employee : BaseEntity
{
  [Key]
  [Column("id")]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Required]
  [StringLength(100)]
  [Column("username")]
  public int Username { get; set; } = RandomId.Generate();
  
  [Required]
  [Column("salary")]
  public decimal Salary { get; set; }
  
  [Column("email")]
  [MaxLength(50)]
  public string? Email { get; set; }
  
  [Column("phone")]
  [MaxLength(30)]
  public string? Phone { get; set; }
  
  [Column("userId")]
  [Required]
  public required Guid ManagerId { get; set; }

  [Required]
  [Column("storeId")]
  public required Guid StoreId { get; set; } 
  
  [ForeignKey(nameof(StoreId))]
  public Store? Store { get; set; }
  
  [ForeignKey(nameof(ManagerId))]
  public Manager? Manager { get; set; }
  
  [Column("birth_date")]
  public DateTime? BirthDate { get; set; } = null;
  
}