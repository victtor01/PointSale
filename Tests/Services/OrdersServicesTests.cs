using System.Collections;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using PointSaleApi.Src.Core.Application.Dtos;
using PointSaleApi.Src.Core.Application.Interfaces;
using PointSaleApi.Src.Core.Application.Records;
using PointSaleApi.Src.Core.Application.Services;
using PointSaleApi.Src.Core.Domain;
using PointSaleApi.Src.Infra.Config;
using PointSaleApi.Src.Infra.Extensions;
using PointSaleApi.Src.Infra.Repositories;

namespace Tests.Services;

[TestClass]
public class OrdersServicesTests
{
  private readonly Mock<IOrdersRepository> _ordersRepositoryMock;
  private readonly Mock<ITablesRepository> _tablesRepositoryMock;
  private readonly IOrdersService _ordersService;

  public OrdersServicesTests()
  {
    _ordersRepositoryMock = new Mock<IOrdersRepository>();
    _tablesRepositoryMock = new Mock<ITablesRepository>();

    _ordersService = new OrdersService(
      tablesRepository: _tablesRepositoryMock.Object,
      ordersRepository: _ordersRepositoryMock.Object);
  }

  [TestMethod]
  public async Task ShouldErrorBacauseTableNotFound()
  {
    CreateOrderDTO createOrderDto = new CreateOrderDTO() { TableId = Guid.NewGuid() };

    _tablesRepositoryMock.Setup(repo => repo.FindByIdAsync(Guid.NewGuid())).ReturnsAsync((StoreTable?)null);

    var anyGuid = It.IsAny<Guid>();
    var exception = await Assert.ThrowsExceptionAsync<NotFoundException>(
      async () => await _ordersService.CreateAsync(createOrderDto, anyGuid, anyGuid));

    _tablesRepositoryMock.Verify(repo => repo.FindByIdAsync(createOrderDto.TableId), Times.Exactly(1));
    _ordersRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<Order>()), Times.Never);

    Assert.AreEqual("Table not found", exception.Message);
  }

  [TestMethod]
  public async Task ShouldCreateANewOrder()
  {
    CreateOrderDTO createOrderDto = new CreateOrderDTO() { TableId = Guid.NewGuid() };
    var anyGuid = It.IsAny<Guid>();
    Guid managerId = Guid.NewGuid();
    Guid storeId = Guid.NewGuid();

    _tablesRepositoryMock.Setup(repo => repo.FindByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
      new StoreTable() { Number = 1, ManagerId = managerId, StoreId = storeId });

    _ordersRepositoryMock.Setup(repo => repo
        .FindAllByManagerAndTableAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
      .ReturnsAsync(new List<Order>() { });

    _ordersRepositoryMock.Setup(repo => repo.SaveAsync(It.IsAny<Order>()))
      .ReturnsAsync(new Order()
      {
        Id = Guid.NewGuid(), TableId = createOrderDto.TableId, ManagerId = managerId, StoreId = storeId
      });

    var res = await _ordersService.CreateAsync(createOrderDto, Guid.NewGuid(), Guid.NewGuid());
    _tablesRepositoryMock.Verify(repo => repo.FindByIdAsync(It.IsAny<Guid>()), Times.Once);

    res.Should().NotBeNull();
  }
}