using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;

namespace Gateways.Api.MapperProfiles;

public class GatewayProfile : Profile
{
    public GatewayProfile()
    {
        CreateMap<Gateway, GatewayGetModel>();
        CreateMap<Gateway, GatewayGetDetailsModel>();
        CreateMap<GatewayPostModel, Gateway>();
        CreateMap<GatewayPutModel, Gateway>();
    }
}
