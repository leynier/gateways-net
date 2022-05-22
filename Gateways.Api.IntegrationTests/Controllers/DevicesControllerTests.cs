using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using System.Net;

namespace Gateways.Api.IntegrationTests.Controllers;

public class DevicesControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, -1)]
    public async Task GetAll_InvalidPageAndPageSize(int page, int pageSize)
    {
        var httpResponse = await client
            .GetAsync($"/api/devices?page={page}&pageSize={pageSize}")
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }

    [Fact]
    public async Task Post_Valid()
    {
        var gatewayModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayModel.ToHttpContent())
            .ConfigureAwait(false);
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        var deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = gateway.Id,
        };
        httpResponse = await client
            .PostAsync("/api/devices", deviceModel.ToHttpContent())
            .ConfigureAwait(false);
        var device = await httpResponse
            .Parse<Device>()
            .ConfigureAwait(false);
        Assert.Equal(deviceModel.Vendor, device.Vendor);
        Assert.Equal(gateway.Id, device.GatewayId);
    }

    [Fact]
    public async Task Put_Valid()
    {
        var gatewayModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayModel.ToHttpContent())
            .ConfigureAwait(false);
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        var deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = gateway.Id,
        };
        httpResponse = await client
            .PostAsync("/api/devices", deviceModel.ToHttpContent())
            .ConfigureAwait(false);
        var device = await httpResponse
            .Parse<Device>()
            .ConfigureAwait(false);
        Assert.Equal(deviceModel.Vendor, device.Vendor);
        var deviceModelUpdate = new DevicePutModel
        {
            Vendor = "Test Device Update",
            GatewayId = gateway.Id,
        };
        httpResponse = await client
            .PutAsync($"/api/devices/{device.Id}", deviceModelUpdate.ToHttpContent())
            .ConfigureAwait(false);
        device = await httpResponse
            .Parse<Device>()
            .ConfigureAwait(false);
        Assert.Equal(deviceModelUpdate.Vendor, device.Vendor);
    }

    [Fact]
    public async Task Post_MaxDevices()
    {
        var gatewayModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        var deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = gateway.Id,
        };
        for (int i = 0; i < 10; i++)
        {
            httpResponse = await client
                .PostAsync("/api/devices", deviceModel.ToHttpContent())
                .ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();
            var device = await httpResponse
                .Parse<Device>()
                .ConfigureAwait(false);
            Assert.Equal(deviceModel.Vendor, device.Vendor);
            Assert.Equal(gateway.Id, device.GatewayId);
        }
        httpResponse = await client
            .PostAsync("/api/devices", deviceModel.ToHttpContent())
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }

    [Fact]
    public async Task Put_MaxDevices()
    {
        var gatewayModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        var deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = gateway.Id,
        };
        for (int i = 0; i < 10; i++)
        {
            httpResponse = await client
                .PostAsync("/api/devices", deviceModel.ToHttpContent())
                .ConfigureAwait(false);
            httpResponse.EnsureSuccessStatusCode();
            var device = await httpResponse
                .Parse<Device>()
                .ConfigureAwait(false);
            Assert.Equal(deviceModel.Vendor, device.Vendor);
            Assert.Equal(gateway.Id, device.GatewayId);
        }
        httpResponse = await client
            .PostAsync("/api/gateways", gatewayModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gateway2 = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = gateway2.Id,
        };
        httpResponse = await client
            .PostAsync("/api/devices", deviceModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var lastDevice = await httpResponse
            .Parse<Device>()
            .ConfigureAwait(false);
        var deviceModelUpdate = new DevicePutModel
        {
            Vendor = "Test Device Update",
            GatewayId = gateway.Id,
        };
        httpResponse = await client
            .PutAsync($"/api/devices/{lastDevice.Id}", deviceModelUpdate.ToHttpContent())
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }

    [Fact]
    public async Task Post_InvalidGatewayId()
    {
        var deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = Guid.NewGuid().ToString(),
        };
        var httpResponse = await client
            .PostAsync("/api/devices", deviceModel.ToHttpContent())
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }

    [Fact]
    public async Task Put_InvalidGatewayId()
    {
        var gatewayModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        var deviceModel = new DevicePostModel
        {
            Vendor = "Test Device",
            GatewayId = gateway.Id,
        };
        httpResponse = await client
            .PostAsync("/api/devices", deviceModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var device = await httpResponse
            .Parse<Device>()
            .ConfigureAwait(false);
        Assert.Equal(deviceModel.Vendor, device.Vendor);
        Assert.Equal(gateway.Id, device.GatewayId);
        var deviceModelUpdate = new DevicePutModel
        {
            Vendor = "Test Device",
            GatewayId = Guid.NewGuid().ToString(),
        };
        httpResponse = await client
            .PutAsync($"/api/devices/{device.Id}", deviceModelUpdate.ToHttpContent())
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }
}
