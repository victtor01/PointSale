using FluentAssertions;
using Moq;
using PointSaleApi.Src.Core.Application.Dtos.StoresDtos;
using PointSaleApi.Src.Core.Application.Interfaces.StoresInterfaces;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace Tests.ServicesTests
{
  [TestClass]
  public class StoresServiceTests
  {
    private readonly Mock<IStoresRepository> _mockStoresRepository;
    private readonly StoresService _storesService;

    public StoresServiceTests()
    {
      _mockStoresRepository = new Mock<IStoresRepository>();
      _storesService = new StoresService(storesRepository: _mockStoresRepository.Object);
    }

    [TestMethod]
    [Description("should create a store success")]
    public async Task ShouldCreateStore()
    {
      CreateStoreDto createStoreDto = new() { Name = "Example" };
      Guid managerId = Guid.NewGuid();

      Store storeMock = new() { Name = createStoreDto.Name, ManagerId = managerId };

      _mockStoresRepository
        .Setup(repo => repo.FindAllByManagerAsync(managerId))
        .ReturnsAsync([new Store { Name = "outher store", ManagerId = managerId }]);

      _mockStoresRepository
        .Setup(repo => repo.SaveAsync(It.IsAny<Store>()))
        .ReturnsAsync(() => storeMock);

      var savedStore = await _storesService.SaveAsync(createStoreDto, managerId);

      Assert.IsNotNull(savedStore);
      Assert.AreEqual(savedStore, storeMock);
      _mockStoresRepository.Verify(repo => repo.SaveAsync(It.IsAny<Store>()), Times.Once);
    }

    [TestMethod]
    [Description("it should throw error because the name is already used")]
    public async Task ShouldErrorWhenNameOfStore()
    {
      CreateStoreDto createStoreDto = new() { Name = "Example" };
      Guid managerId = Guid.NewGuid();

      _mockStoresRepository
        .Setup(repo => repo.FindAllByManagerAsync(managerId))
        .ReturnsAsync([new Store { Name = createStoreDto.Name, ManagerId = managerId }]);

      BadRequestException exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
        () => _storesService.SaveAsync(createStoreDto, managerId)
      );

      Assert.AreEqual("you have a store with that name", exception.Message);
    }
  }
}
