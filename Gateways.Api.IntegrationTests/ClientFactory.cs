using Microsoft.AspNetCore.Mvc.Testing;

namespace Gateways.Api.IntegrationTests;

public static class ClientFactory
{
    public static HttpClient? CreateClient() => new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder => { })
            .CreateClient();
}
