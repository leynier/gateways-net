using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Business.Implementations.Repositories;

namespace Gateways.Business.Implementations.Services;

public class DeviceService : BaseService<Device>, IDeviceService
{
    public DeviceService(IDeviceRepository repository) : base(repository)
    {
    }
}
