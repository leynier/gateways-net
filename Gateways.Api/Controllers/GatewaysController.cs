using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Common.Controllers;

namespace Gateways.Api.Controllers;

public class GatewaysController : CrudApiControllerBase<Gateway, GatewayGetModel, GatewayPostModel, GatewayPutModel>
{
    public GatewaysController(IGatewayUseCase useCase, IMapper mapper) : base(useCase, mapper)
    {
    }
}