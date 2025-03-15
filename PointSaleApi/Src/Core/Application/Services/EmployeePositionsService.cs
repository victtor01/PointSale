using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Infra.Interfaces;

namespace PointSaleApi.Src.Core.Application.Services;

public class EmployeePositionsService(IPositionsRepository positionsRepository) : IEmployeePositionsService
{
  private readonly IPositionsRepository _positionsRepository = positionsRepository;

  public async Task<EmployeePosition> GetByIdAsync(Guid managerId, Guid positionId)
  {
    var position = await _positionsRepository.GetById(positionId)
                   ?? throw new NotFoundException("position not found!");

    if (position.ManagerId != managerId)
      throw new BadRequestException("Employee position does not belong to this manager!");

    return position;
  }

  public async Task<EmployeePosition> UpdateAsync
    (Guid managerId, Guid positionId, UpdateEmployeePositionRecord updateEmployeePositionRecord)
  {
    EmployeePosition position = await _positionsRepository.GetById(positionId)
                                ?? throw new NotFoundException("position not found!");

    position.Name = updateEmployeePositionRecord.Name;

    HashSet<string> newPermissions = new HashSet<string>(updateEmployeePositionRecord.permissions);
    position.SetPermissions(newPermissions);
    
    await _positionsRepository.Update(position);
    
    return position;
  }

  public async Task<EmployeePosition> CreateAsync(
    CreateEmployeePositionDTO createEmployeePositionDto,
    Guid ManagerId, Guid storeId)
  {
    var positionInDB = await _positionsRepository
      .FindByNameAndManagerAsync(createEmployeePositionDto.Name, ManagerId);

    if (positionInDB != null)
      throw new BadRequestException("Você já tem um cargo com esse nome");

    EmployeePosition employeePosition = new EmployeePosition()
    {
      Name = createEmployeePositionDto.Name,
      ManagerId = ManagerId,
      StoreId = storeId
    };

    employeePosition.SetPermissions(createEmployeePositionDto.Permissions);

    return await _positionsRepository.Create(employeePosition);
  }

  public async Task<List<EmployeePosition>> GetAllAsync(Guid ManagerId, Guid storeId)
  {
    var positions = await this._positionsRepository
      .FindAllByManagerAndStoreIdAsync(ManagerId, storeId);

    return positions;
  }
}