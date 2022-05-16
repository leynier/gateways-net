using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Api.Controllers;

public class DevicesController : CrudApiControllerBase<Device, DeviceGetModel, DeviceGetDetailsModel, DevicePostModel, DevicePutModel, int>
{
    public DevicesController(IDeviceService service, IMapper mapper)
        : base(service, mapper, q => q.Include(d => d.Gateway))
    {
    }
}
