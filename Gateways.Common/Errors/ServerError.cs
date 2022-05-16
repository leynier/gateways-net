namespace Gateways.Common.Errors;

public class ServerError : Exception
{
    public ServerError(string message = "Internal Server Error") : base(message)
    {
    }
}