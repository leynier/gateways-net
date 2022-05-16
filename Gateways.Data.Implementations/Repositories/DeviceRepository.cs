using Gateways.Business.Contracts;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Implementations.Repositories;

namespace Gateways.Data.Implementations.Repositories
{
    public class DeviceRepository : BaseRepository<Device, int>, IDeviceRepository
    {
        public DeviceRepository(IObjectContext context) : base(context)
        {
        }
    }
}
