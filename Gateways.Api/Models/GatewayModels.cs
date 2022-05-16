namespace Gateways.Api.Models;

public class GatewayBaseModel
{
    public string Name { get; set; }
}

public class GatewayGetModel : GatewayBaseModel
{
    public string Id { get; set; }
}

public class GatewayPostModel : GatewayBaseModel
{
    
}

public class GatewayPutModel : GatewayBaseModel
{

}
