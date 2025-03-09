namespace PointSaleApi.Src.Core.Application.Records;

public record CreateEmployeeDTO(
  string FirstName,
  decimal Salary,
  string Password,
  List<Guid> Positions,
  string? LastName
);