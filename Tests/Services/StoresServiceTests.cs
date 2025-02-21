using FluentAssertions;
using Moq;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;

namespace Tests.Services;

[TestClass]
public class StoresServiceTests
{
  private readonly Mock<IStoresRepository> _mockStoresRepository;
  private readonly IStoresService _storesService;

  public StoresServiceTests()
  {
    _mockStoresRepository = new Mock<IStoresRepository>();
    _storesService = new StoresService(_storesRepository: _mockStoresRepository.Object);
  }

  [TestMethod]
  [Description("should create a store success")]
  public async Task ShouldCreateStore()
  {
    CreateStoreDTO createStoreDto = new() { Name = "Example" };
    Guid managerId = Guid.NewGuid();

    Store storeMock = new() { Name = createStoreDto.Name, ManagerId = managerId, Password = "EXAMPLEPASSWORD" };

    _mockStoresRepository
      .Setup(repo => repo.FindAllByManagerAsync(managerId))
      .ReturnsAsync([new Store { Name = "outher store", ManagerId = managerId, Password = "EXAMPLEPASSWORD" }]);

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
    CreateStoreDTO createStoreDto = new() { Name = "Example" };
    Guid managerId = Guid.NewGuid();

    _mockStoresRepository
      .Setup(repo => repo.FindAllByManagerAsync(managerId))
      .ReturnsAsync([new Store { Name = createStoreDto.Name, ManagerId = managerId, Password = "EXAMPLEPASSWORD" }]);

    BadRequestException exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
      () => _storesService.SaveAsync(createStoreDto, managerId)
    );

    exception.Message.Should().Contain("you have a tore with that name");
  }

  [TestMethod]
  [Description("it should throw error because the store count")]
  public async Task ShouldErrorWhenNameIsInvalid()
  {
    CreateStoreDTO createStoreDto = new() { Name = "Example" };
    Guid managerId = Guid.NewGuid();

    _mockStoresRepository.Setup(repo => repo.FindAllByManagerAsync(managerId))
      .ReturnsAsync(new List<Store>() { new Store() { ManagerId = managerId, Name = createStoreDto.Name } });

    var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
      () => _storesService.SaveAsync(createStoreDto, managerId)
    );
    
    _mockStoresRepository.Verify(repo => repo.SaveAsync(It.IsAny<Store>()), Times.Never);

    exception.Should().BeOfType<BadRequestException>();
    exception.Message.Should().Be("you have a tore with that name");
  }

  [TestMethod]
  [Description("it should throw error because count of stores is invalid")]
  public async Task ShouldErrorWhenStoreCountIsInvalid()
  {
    var managerId = Guid.NewGuid();
    var createStoreDto = new CreateStoreDTO { Name = "Example", Password = "password123", Revenue = 1000 };
    var stores = new List<Store>
    {
      new Store { Name = "Store 1", ManagerId = managerId },
      new Store { Name = "Store 2", ManagerId = managerId },
      new Store { Name = "Store 3", ManagerId = managerId },
      new Store { Name = "Store 4", ManagerId = managerId }
    };
    
    _mockStoresRepository.Setup(repo => repo.FindAllByManagerAsync(managerId)).ReturnsAsync(stores);
    var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(
      () => _storesService.SaveAsync(createStoreDto, managerId)
    );
    
    _mockStoresRepository.Verify(repo => repo.SaveAsync(It.IsAny<Store>()), Times.Never);
    
    exception.Should().BeOfType<BadRequestException>();
    exception.Message.Should().Be("count of stores!");
  }
  
}