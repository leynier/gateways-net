using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;

namespace Gateways.Api.MapperProfiles;

public class DeviceProfile : Profile
{
    public DeviceProfile()
    {
        CreateMap<Device, DeviceGetModel>();
        CreateMap<Device, DeviceGetDetailsModel>();
        CreateMap<DevicePostModel, Device>();
        CreateMap<DevicePutModel, Device>();
    }
}
