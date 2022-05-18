using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Api.Controllers;

public class GatewaysController : CrudApiControllerBase<Gateway, GatewayGetModel, GatewayGetDetailsModel, GatewayPostModel, GatewayPutModel, string>
{
    public GatewaysController(IGatewayService service, IMapper mapper)
        : base(service, mapper, q => q.Include(g => g.Devices), q => q.OrderBy(g => g.Name))
    {
    }
}
