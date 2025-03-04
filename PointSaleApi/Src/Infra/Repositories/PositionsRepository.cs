using Microsoft.EntityFrameworkCore;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Database;
using PointSaleApi.Src.Infra.Interfaces;

namespace PointSaleApi.Src.Infra.Repositories;

public class PositionsRepository(DatabaseContext databaseContext) : IPositionsRepository
{
  private readonly DatabaseContext _databaseContext = databaseContext;
  
  public async Task<EmployeePosition> Create(EmployeePosition employeePosition)
  {
    var add = await _databaseContext.EmployeePositions.AddAsync(employeePosition);
    _databaseContext.SaveChanges();
    return add.Entity;
  }

  public async Task<EmployeePosition?> GetById(Guid id)
  {
    return await _databaseContext.EmployeePositions.FirstOrDefaultAsync(e => e.Id == id) ?? null; 
  }
}