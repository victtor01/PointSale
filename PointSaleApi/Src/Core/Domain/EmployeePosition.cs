using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PointSaleApi.Src.Core.Domain;

[Table("employee_position")]
public class EmployeePosition
{
  [Key]
  public Guid Id { get; set; } = Guid.NewGuid();
  
  [Column("name")]
  [MaxLength(100)]
  public required string Name { get; set; }
  
  [Column("employeeId")]
  public required Guid EmployeeId { get; set; }

  [Column("employeeId")]
  public required Guid ManagerId { get; set; }

  [ForeignKey(nameof(EmployeeId))]
  public Employee? Employee { get; set; }

  [ForeignKey(nameof(ManagerId))]
  public Employee? Manager { get; set; }
  
  [Column("permissions_orders")]
  public required List<string> PermissionsOrders { get; set; }

  [Column("permissions_stores")] 
  public required List<string> PermissionsStores { get; set; } = [];
}

public static class EmployeePermissionOrders
{
  public const string CREATE = "CREATE";
  public const string UPDATE_PENDING = "UPDATE_PENDING";
  public const string UPDATE_PROCESSING = "UPDATE_PROCESSING";
  public const string UPDATE_COMPLETED = "UPDATE_COMPLETED";
  public const string UPDATE_CANCELLED = "UPDATE_CANCELLED";
  public const string DELETE = "DELETE";
}

public static class EmployeePermissionStores
{
  public const string DELETE = "DELETE";
}

// public enum EmployeePermissionOrders
// {
//   CREATE = 0,
//   UPDATE_PENDING = 1, 
//   UPDATE_PROCESSING = 2,
//   UPDATE_COMPLETED = 3, 
//   UPDATE_CANCELLED = 4,
//   DELETE = 100,
// }

//
// public enum EmployeePermissionStores
// {
//   CREATE = 0,
//   UPDATE = 1,
//   DELETE = 2,
// }