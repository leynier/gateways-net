namespace Gateways.Api.Models;

public class GatewayBaseModel
{
    public string Name { get; set; } = default!;
}

public class GatewayGetModel : GatewayBaseModel
{
    public string Id { get; set; } = default!;
}

public class GatewayPostModel : GatewayBaseModel
{
    
}

public class GatewayPutModel : GatewayBaseModel
{

}
