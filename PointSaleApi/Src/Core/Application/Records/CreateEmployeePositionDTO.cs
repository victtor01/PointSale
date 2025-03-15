namespace PointSaleApi.Src.Core.Application.Records;

public record CreateEmployeePositionDTO(HashSet<string> Permissions, string Name);