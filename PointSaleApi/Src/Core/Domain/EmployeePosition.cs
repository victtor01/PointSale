using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PointSaleApi.Src.Infra.Config;

namespace PointSaleApi.Src.Core.Domain;

[Table("employee_position")]
public class EmployeePosition
{
  [Key] public Guid Id { get; set; } = Guid.NewGuid();

  [Column("name")] [MaxLength(100)] 
  public required string Name { get; set; }

  [Column("managerId")] 
  public required Guid ManagerId { get; set; }

  [ForeignKey(nameof(ManagerId))] 
  public  Manager? Manager { get; set; }

  [Required] [Column("permissions_orders")]
  public List<string> Permissions { get; private set; } = [];
  
  [Required] [Column("storeId")]
  public Guid? StoreId { get; set; }
  
  [ForeignKey(nameof(StoreId))]
  public Store? Store { get; set; }

  public void SetPermissions(HashSet<string> permissions)
  {
    var parsedPermissions = permissions
      .Where(permission =>
        Enum.IsDefined(typeof(EmployeePermissionOrders), permission)
        || Enum.IsDefined(typeof(EmployeePermissionsProducts), permission))
      .ToHashSet();

    if (parsedPermissions.Count != permissions.Count)
      throw new BadRequestException("Invalid permission(s) detected.");

    Permissions = parsedPermissions.ToList();
  }
}

public enum EmployeePermissionOrders
{
  CREATE_ORDER = 0,
  UPDATE_ORDER_PADDING = 1,
  UPDATE_ORDER_PROCESSING = 2,
  UPDATE_ORDER_COMPLETED = 3,
  UPDATE_ORDER_CANCELLED = 4,
  DELETE_ORDER = 99,
}

public enum EmployeePermissionsProducts
{
  CREATE_PRODUCT = 100,
  DELETE_PRODUCT = 101,
  UPDATE_PRODUCT = 102
}