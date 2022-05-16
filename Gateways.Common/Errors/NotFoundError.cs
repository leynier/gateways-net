namespace Gateways.Common.Errors;

public class NotFoundError : Exception
{
    public NotFoundError(string message = "Not found") : base(message)
    {
    }
}