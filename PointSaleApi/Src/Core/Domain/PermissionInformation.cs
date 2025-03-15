namespace PointSaleApi.Src.Core.Domain;

public class PermissionInformation
{
  public  string EnumName { get; set; }
  public  string Name { get; set; }
  public  string Description { get; set; }
  
  public PermissionInformation(string enumName, string name, string description)
  {
    EnumName = enumName;
    Name = name;
    Description = description;
  }
}