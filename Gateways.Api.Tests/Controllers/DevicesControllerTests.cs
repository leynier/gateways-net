using AutoMapper;
using Gateways.Api.Controllers;
using Gateways.Api.Tests;
using Gateways.Business.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Devices.Api.Tests.Controllers;

public class DevicesControllerTests
{
    private readonly IConfiguration config;
    private readonly Mock<IDeviceService> deviceService;
    private readonly IMapper mapper;

    public DevicesControllerTests()
    {
        config = new ConfigurationBuilder().Build();
        deviceService = new Mock<IDeviceService>();
        mapper = AutoMapperFactory.CreateMapper();
    }

    private DevicesController Controller => new(deviceService.Object, mapper, config);
}
