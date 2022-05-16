using Gateways.Business.Contracts;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Implementations.Repositories;

namespace Gateways.Data.Implementations.Repositories
{
    public class GatewayRepository : BaseRepository<Gateway>, IGatewayRepository
    {
        public GatewayRepository(IObjectContext context) : base(context)
        {
        }
    }
}
