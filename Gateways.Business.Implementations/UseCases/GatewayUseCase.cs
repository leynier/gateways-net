using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Business.Implementations.Repositories;

namespace Gateways.Business.Implementations.UseCases;

public class GatewayUseCase : BaseUseCase<Gateway>, IGatewayUseCase
{
    public GatewayUseCase(IGatewayRepository repository) : base(repository)
    {
    }
}
