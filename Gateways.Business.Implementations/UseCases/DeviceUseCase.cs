using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Business.Implementations.Repositories;

namespace Gateways.Business.Implementations.UseCases;

public class DeviceUseCase : BaseUseCase<Device>, IDeviceUseCase
{
    public DeviceUseCase(IDeviceRepository repository) : base(repository)
    {
    }
}
