namespace Gateways.Api.IntegrationTests.Controllers;

public class BaseControllerTests
{
    protected readonly HttpClient client;

    public BaseControllerTests()
    {
        var client = ClientFactory.CreateClient();
        if (client == null)
            throw new Exception("Client is null");
        this.client = client;
    }
}
