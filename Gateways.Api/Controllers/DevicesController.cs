using AutoMapper;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.UseCases;
using Gateways.Common.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Api.Controllers;

public class DevicesController : CrudApiControllerBase<Device, DeviceGetModel, DeviceGetDetailsModel, DevicePostModel, DevicePutModel, int>
{
    public DevicesController(IDeviceUseCase useCase, IMapper mapper)
        : base(useCase, mapper, q => q.Include(d => d.Gateway))
    {
    }
}
