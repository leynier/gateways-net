using AutoMapper;
using Gateways.Api.Controllers;
using Gateways.Api.Models;
using Gateways.Api.Tests;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Errors;
using Gateways.Common.Models;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Devices.Api.Tests.Controllers;

public class DevicesControllerTests
{
    private readonly IConfiguration config;
    private readonly Mock<IDeviceService> deviceService;
    private readonly Mock<IGatewayService> gatewayService;
    private readonly IMapper mapper;

    public DevicesControllerTests()
    {
        config = new ConfigurationBuilder().Build();
        deviceService = new Mock<IDeviceService>();
        gatewayService = new Mock<IGatewayService>();
        mapper = AutoMapperFactory.CreateMapper();
    }

    private DevicesController Controller => new(gatewayService.Object, deviceService.Object, mapper, config);

    private static IQueryable<Device> GetQueryableWithData(Gateway gateway, int count)
    {
        var devices = new List<Device>();
        for (int i = 0; i < count; i++)
        {
            devices.Add(new Device
            {
                Id = i,
                Vendor = $"Device {i}",
                Gateway = gateway,
                GatewayId = gateway.Id,
            });
        }
        return devices.AsQueryable();
    }

    [Fact]
    public void Post_RaiseError_WhenGatewayHasMoreThan10Devices()
    {
        // Arrange
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(GetQueryableWithData(gateway, 10));

        // Act
        var devicePostModel = new DevicePostModel { Vendor = "Device 11", GatewayId = gateway.Id };
        var action = () => Controller.Post(devicePostModel);

        // Assert
        Assert.Throws<BadRequestError>(action);
    }

    [Fact]
    public void Put_RaiseError_WhenGatewayHasMoreThan10Devices()
    {
        // Arrange
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var gateway2 = new Gateway { Name = "Gateway 2", IPv4 = "127.0.0.2" };
        var devices = GetQueryableWithData(gateway, 11).ToList();
        var device = devices[^1];
        device.GatewayId = gateway2.Id;
        device.Gateway = gateway2;
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(devices.AsQueryable());

        // Act
        var devicePutModel = new DevicePutModel { Vendor = "Device 11", GatewayId = gateway.Id };
        var action = () => Controller.Put(device.Id, devicePutModel);

        // Assert
        Assert.Throws<BadRequestError>(action);
    }

    [Fact]
    public void GetAll_ShouldReturnEmptyResult()
    {
        // Arrange
        var deviceQueryable = Array.Empty<Device>().AsQueryable();
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

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
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 10);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

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
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 10);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

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
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 10);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

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
        var deviceQueryable = Array.Empty<Device>().AsQueryable();
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

        // Act
        var action = () => Controller.Get(0);

        // Assert
        Assert.Throws<NotFoundError>(action);
    }

    [Fact]
    public void Get_ShouldReturnGateway()
    {
        // Arrange
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 5);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

        // Act
        var result = Controller.Get(deviceQueryable.First().Id);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(deviceQueryable.First().Vendor, result.Data!.Vendor);
    }

    [Fact]
    public void Put_ShouldReturnNotFound()
    {
        // Arrange
        var deviceQueryable = Array.Empty<Device>().AsQueryable();
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

        // Act
        var action = () => Controller.Put(0, new DevicePutModel());

        // Assert
        Assert.Throws<NotFoundError>(action);
    }

    [Fact]
    public void Put_ShouldChangeName()
    {
        // Arrange
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 5);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);
        var gatewayQueryable = new[] { gateway }.AsQueryable();
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.Put(deviceQueryable.First().Id, new DevicePutModel
        {
            Vendor = "New Vendor",
            GatewayId = gateway.Id
        });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal("New Vendor", result.Data!.Vendor);

        deviceService.Verify(x => x.Update(It.IsAny<Device>()), Times.Once);
    }

    [Fact]
    public void Post_ShouldReturnGateway()
    {
        // Arrange
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 5);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);
        var gatewayQueryable = new[] { gateway }.AsQueryable();
        gatewayService.Reset();
        gatewayService.Setup(x => x.Query()).Returns(gatewayQueryable);

        // Act
        var result = Controller.Post(new DevicePostModel
        {
            Vendor = "New Vendor",
            GatewayId = gateway.Id
        });

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal("New Vendor", result.Data!.Vendor);

        deviceService.Verify(x => x.Add(It.IsAny<Device>()), Times.Once);
    }

    [Fact]
    public void Delete_ShouldReturnNotFound()
    {
        // Arrange
        var deviceQueryable = Array.Empty<Device>().AsQueryable();
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

        // Act
        var action = () => Controller.Delete(0);

        // Assert
        Assert.Throws<NotFoundError>(action);
    }

    [Fact]
    public void Delete_ShouldDeleteGateway()
    {
        // Arrange
        var gateway = new Gateway { Name = "Gateway 1", IPv4 = "127.0.0.1" };
        var deviceQueryable = GetQueryableWithData(gateway, 5);
        deviceService.Reset();
        deviceService.Setup(x => x.Query()).Returns(deviceQueryable);

        // Act
        var result = Controller.Delete(deviceQueryable.First().Id);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.Equal(deviceQueryable.First().Vendor, result.Data!.Vendor);

        deviceService.Verify(x => x.Remove(It.IsAny<Device>()), Times.Once);
    }
}
