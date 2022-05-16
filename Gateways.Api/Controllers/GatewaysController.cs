using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Common.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Api.Controllers;

public class GatewaysController : CrudApiControllerBase<Gateway, GatewayGetModel, GatewayGetDetailsModel, GatewayPostModel, GatewayPutModel, string>
{
    public GatewaysController(IGatewayUseCase useCase, IMapper mapper)
        : base(useCase, mapper, q => q.Include(g => g.Devices))
    {
    }
}
