namespace PointSaleApi.Src.Core.Application.Records;

public record CreateEmployeeDTO(
  decimal Salary,
  string Password,
  List<Guid> Positions
);