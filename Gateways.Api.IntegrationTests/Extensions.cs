using Gateways.Common.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Gateways.Api.IntegrationTests;

public static class Extensions
{
    public static async Task<T> Parse<T>(this HttpResponseMessage httpResponse)
    {
        var responseBody = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        var response = JsonConvert.DeserializeObject<Response<T>>(responseBody);
        Assert.NotNull(response);
        Assert.StrictEqual(response.StatusCode, (int)httpResponse.StatusCode);
        Assert.NotNull(response.Data);
        return response.Data!;
    }

    public static async Task<Response<T>> ParseRaw<T>(this HttpResponseMessage httpResponse)
    {
        var responseBody = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
        var response = JsonConvert.DeserializeObject<Response<T>>(responseBody);
        Assert.NotNull(response);
        Assert.StrictEqual(response.StatusCode, (int)httpResponse.StatusCode);
        return response;
    }

    public static HttpContent ToHttpContent<T>(this T obj)
    {
        var content = JsonConvert.SerializeObject(obj);
        var buffer = Encoding.UTF8.GetBytes(content);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return byteContent;
    }
        
}
