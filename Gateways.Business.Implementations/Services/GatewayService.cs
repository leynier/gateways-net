using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Business.Implementations.Repositories;

namespace Gateways.Business.Implementations.Services;

public class GatewayService : BaseService<Gateway>, IGatewayService
{
    public GatewayService(IGatewayRepository repository) : base(repository)
    {

    }
}
