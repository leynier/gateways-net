using AutoMapper;
using Gateways.Api.MapperProfiles;

namespace Gateways.Api.Tests;

public static class AutoMapperFactory
{
    public static IMapper CreateMapper()
    {
        return new MapperConfiguration(config =>
        {
            config.AddProfile(new DeviceProfile());
            config.AddProfile(new GatewayProfile());
        }).CreateMapper();
    }
}
