using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using System.Net;

namespace Gateways.Api.IntegrationTests.Controllers;

public class GatewaysControllerTests : BaseControllerTests
{
    [Theory]
    [InlineData(-1, 1)]
    [InlineData(0, -1)]
    public async Task GetAll_InvalidPageAndPageSize(int page, int pageSize)
    {
        var httpResponse = await client
            .GetAsync($"/api/gateways?page={page}&pageSize={pageSize}")
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }

    [Fact]
    public async Task CreateEditGetAndDelete_ValidGateway()
    {
        var gatewayPostModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayPostModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        Assert.Equal(gatewayPostModel.Name, gateway.Name);
        Assert.Equal(gatewayPostModel.IPv4, gateway.IPv4);
        var gatewayPutModel = new GatewayPutModel
        {
            Name = "Test Gateway Edited",
            IPv4 = gateway.IPv4,
        };
        httpResponse = await client
            .PutAsync($"/api/gateways/{gateway.Id}", gatewayPutModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gatewayEdited = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        Assert.Equal(gatewayPutModel.Name, gatewayEdited.Name);
        Assert.Equal(gatewayPutModel.IPv4, gatewayEdited.IPv4);
        Assert.Equal(gateway.Id, gatewayEdited.Id);
        Assert.NotEqual(gateway.Name, gatewayEdited.Name);
        Assert.Equal(gateway.IPv4, gatewayEdited.IPv4);
        httpResponse = await client
            .GetAsync($"/api/gateways/{gateway.Id}")
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        httpResponse = await client
            .DeleteAsync($"/api/gateways/{gateway.Id}")
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        httpResponse = await client
            .GetAsync($"/api/gateways/{gateway.Id}")
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
    }

    // Test the ipv4 invalid
    [Theory]
    [InlineData("localhost")]
    [InlineData("127")]
    [InlineData("127.0.0")]
    [InlineData("259.0.0.-1")]
    [InlineData("")]
    [InlineData(null)]
    public async Task Create_InvalidGateway(string ipv4)
    {
        var gatewayPostModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = ipv4,
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayPostModel.ToHttpContent())
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }

    [Theory]
    [InlineData("localhost")]
    [InlineData("127")]
    [InlineData("127.0.0")]
    [InlineData("259.0.0.-1")]
    [InlineData("")]
    [InlineData(null)]
    public async Task Edit_InvalidGateway(string ipv4)
    {
        var gatewayPostModel = new GatewayPostModel
        {
            Name = "Test Gateway",
            IPv4 = "127.0.0.1",
        };
        var httpResponse = await client
            .PostAsync("/api/gateways", gatewayPostModel.ToHttpContent())
            .ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();
        var gateway = await httpResponse
            .Parse<Gateway>()
            .ConfigureAwait(false);
        var gatewayPutModel = new GatewayPutModel
        {
            Name = "Test Gateway Edited",
            IPv4 = ipv4,
        };
        httpResponse = await client
            .PutAsync($"/api/gateways/{gateway.Id}", gatewayPutModel.ToHttpContent())
            .ConfigureAwait(false);
        Assert.Equal(HttpStatusCode.BadRequest, httpResponse.StatusCode);
    }
}
