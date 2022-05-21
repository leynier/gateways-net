using AutoMapper;
using Gateways.Api.Controllers;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Errors;
using Gateways.Common.Models;
using Moq;

namespace Gateways.Api.Tests.Controllers;

public class GatewaysControllerTests
{
    private readonly Mock<IGatewayService> gatewayService;
    private readonly IMapper mapper;

    public GatewaysControllerTests()
    {
        gatewayService = new Mock<IGatewayService>();
        mapper = AutoMapperFactory.CreateMapper();
    }

    private GatewaysController Controller => new(gatewayService.Object, mapper);

    private IQueryable<Gateway> GetQueryableWithData => new[]
        {
            new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" },
            new Gateway { Name = "Gateway 2", IPv4 = "127.0.0.2" },
            new Gateway { Name = "Gateway 3", IPv4 = "127.0.0.3" },
            new Gateway { Name = "Gateway 4", IPv4 = "127.0.0.4" },
            new Gateway { Name = "Gateway 5", IPv4 = "127.0.0.5" },
            new Gateway { Name = "Gateway 6", IPv4 = "127.0.0.6" },
            new Gateway { Name = "Gateway 7", IPv4 = "127.0.0.7" },
            new Gateway { Name = "Gateway 8", IPv4 = "127.0.0.8" },
            new Gateway { Name = "Gateway 9", IPv4 = "127.0.0.9" },
            new Gateway { Name = "Gateway 10", IPv4 = "127.0.0.10" },
        }.AsQueryable();

    [Fact]
    public void GetAll_ShouldReturnEmptyResult()
    {
        // Arrange
        var gatewayQueryable = Array.Empty<Gateway>().AsQueryable();
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.GetAll(new PaginationQueryModel());

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data!.Items);
        Assert.False(result.Data!.HasPrevious);
        Assert.False(result.Data!.HasNext);
    }

    [Fact]
    public void GetAll_ShouldReturnPaginatedResult()
    {
        // Arrange
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(GetQueryableWithData);

        // Act
        var result = Controller.GetAll(new PaginationQueryModel { Page = 2, PageSize = 3 });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data!.Items.Count());
        Assert.True(result.Data!.HasPrevious);
        Assert.True(result.Data!.HasNext);
    }

    [Fact]
    public void GetAll_ShouldReturnPaginatedResultWithEmptyItems()
    {
        // Arrange
        var gatewayQueryable = GetQueryableWithData;
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.GetAll(new PaginationQueryModel { Page = 4, PageSize = 3 });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data!.Items);
        Assert.True(result.Data!.HasPrevious);
        Assert.False(result.Data!.HasNext);
    }

    [Fact]
    public void GetAll_ShouldReturnPaginatedResultWithEmptyItemsAndNoPrevious()
    {
        // Arrange
        var gatewayQueryable = GetQueryableWithData;
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.GetAll(new PaginationQueryModel { Page = 5, PageSize = 3 });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data!.Items);
        Assert.False(result.Data!.HasPrevious);
        Assert.False(result.Data!.HasNext);
    }

    [Fact]
    public void Get_ShouldReturnNotFound()
    {
        // Arrange
        var gatewayQueryable = Array.Empty<Gateway>().AsQueryable();
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var action = () => Controller.Get(Guid.NewGuid().ToString());

        // Assert
        Assert.Throws<NotFoundError>(action);
    }

    [Fact]
    public void Get_ShouldReturnGateway()
    {
        // Arrange
        var gatewayQueryable = GetQueryableWithData;
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.Get(gatewayQueryable.First().Id);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(gatewayQueryable.First().Name, result.Data!.Name);
    }

    [Fact]
    public void Put_ShouldReturnNotFound()
    {
        // Arrange
        var gatewayQueryable = Array.Empty<Gateway>().AsQueryable();
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var action = () => Controller.Put(Guid.NewGuid().ToString(), new GatewayPutModel());

        // Assert
        Assert.Throws<NotFoundError>(action);
    }

    [Fact]
    public void Put_ShouldChangeName()
    {
        // Arrange
        var gatewayQueryable = GetQueryableWithData;
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.Put(gatewayQueryable.First().Id, new GatewayPutModel { Name = "New Name" });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal("New Name", result.Data!.Name);

        gatewayService.Verify(x => x.Update(It.IsAny<Gateway>()), Times.Once);
    }

    [Fact]
    public void Post_ShouldReturnGateway()
    {
        // Arrange
        var gatewayQueryable = GetQueryableWithData;
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.Post(new GatewayPostModel
        {
            Name = "New Name",
            IPv4 = "127.0.0.1",
        });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal("New Name", result.Data!.Name);

        gatewayService.Verify(x => x.Add(It.IsAny<Gateway>()), Times.Once);
    }

    [Fact]
    public void Delete_ShouldReturnNotFound()
    {
        // Arrange
        var gatewayQueryable = Array.Empty<Gateway>().AsQueryable();
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var action = () => Controller.Delete(Guid.NewGuid().ToString());

        // Assert
        Assert.Throws<NotFoundError>(action);
    }

    [Fact]
    public void Delete_ShouldDeleteGateway()
    {
        // Arrange
        var gatewayQueryable = GetQueryableWithData;
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.Delete(gatewayQueryable.First().Id);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(gatewayQueryable.First().Name, result.Data!.Name);

        gatewayService.Verify(x => x.Remove(It.IsAny<Gateway>()), Times.Once);
    }
}
