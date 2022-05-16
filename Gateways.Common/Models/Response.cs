namespace Gateways.Common.Models;

public class Response<T>
{
    public T? Data { get; set; }

    public string Message { get; set; } = "Success";

    public int StatusCode { get; set; } = 200;
}

public class Response : Response<string>
{
}
