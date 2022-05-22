using AutoMapper;
using Gateways.Api.Helpers;
using Gateways.Api.Models;
using Gateways.Business.Contracts.Entities;
using Gateways.Business.Contracts.Services;
using Gateways.Common.Controllers;
using Gateways.Common.Errors;
using Gateways.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gateways.Api.Controllers;

public class DevicesController : CrudApiControllerBase<Device, DeviceGetModel, DeviceGetDetailsModel, DevicePostModel, DevicePutModel, int>
{
    private readonly int maxNumberOfDevicesPerGateway;
    private readonly IGatewayService gatewayService;

    public DevicesController(
        IGatewayService gatewayService,
        IDeviceService service,
        IMapper mapper,
        IConfiguration config)
        : base(service, mapper, q => q.Include(d => d.Gateway), q => q.OrderBy(d => d.Vendor))
    {
        maxNumberOfDevicesPerGateway = config.GetValue(Configs.MaxNumberOfDevicesPerGateway, 10);
        this.gatewayService = gatewayService;
    }

    [HttpPost]
    public override Response<DeviceGetModel> Post([FromBody] DevicePostModel model)
    {
        var count = service.Query().Where(g => g.GatewayId == model.GatewayId).Count();
        if (count >= maxNumberOfDevicesPerGateway)
            throw new BadRequestError("Maximum number of devices reached for gateway");
        var existingGateway = gatewayService.Query().Any(g => g.Id == model.GatewayId);
        if (!existingGateway)
            throw new BadRequestError("Gateway does not exist");
        return base.Post(model);
    }

    [HttpPut("{id}")]
    public override Response<DeviceGetModel> Put(int id, [FromBody] DevicePutModel model)
    {
        var gatewayId = service
            .Query()
            .Where(g => Equals(g.Id, id))
            .Select(g => g.GatewayId)
            .FirstOrDefault();
        if (gatewayId == null)
            throw new NotFoundError();
        if (gatewayId != model.GatewayId)
        {
            var count = service.Query().Where(g => g.GatewayId == model.GatewayId).Count();
            if (count >= maxNumberOfDevicesPerGateway)
                throw new BadRequestError("Maximum number of devices reached for gateway");
            var existingGateway = gatewayService.Query().Any(g => g.Id == model.GatewayId);
            if (!existingGateway)
                throw new BadRequestError("Gateway does not exist");
        }
        return base.Put(id, model);
    }
}
