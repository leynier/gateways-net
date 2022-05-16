using Gateways.Business.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace Gateways.Api.Models;

public class DeviceBaseModel
{
    [Required]
    public string Vendor { get; set; } = default!;
    [Required]
    public DeviceStatus Status { get; set; } = DeviceStatus.Offline;

    [Required]
    public string GatewayId { get; set; } = default!;
}

public class DeviceGetModel : DeviceBaseModel
{
    public int Id { get; set; } = default!;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}

public class DeviceGetDetailsModel : DeviceGetModel
{
    public GatewayGetModel Gateway { get; set; } = default!;
}

public class DevicePostModel : DeviceBaseModel
{

}

public class DevicePutModel : DeviceBaseModel
{

}
