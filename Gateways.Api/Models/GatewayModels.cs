using Gateways.Common.Validators;
using System.ComponentModel.DataAnnotations;

namespace Gateways.Api.Models;

public class GatewayBaseModel
{
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    [IPv4]
    public string IPv4 { get; set; } = default!;
}

public class GatewayGetModel : GatewayBaseModel
{
    public string Id { get; set; } = default!;
}

public class GatewayGetDetailsModel : GatewayGetModel
{
    public IEnumerable<DeviceGetModel> Devices { get; set; } = new List<DeviceGetModel>();
}

public class GatewayPostModel : GatewayBaseModel
{
    
}

public class GatewayPutModel : GatewayBaseModel
{

}
