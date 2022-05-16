namespace Gateways.Common.Errors;

public class BadRequestError : Exception
{
    public BadRequestError(string message = "Invalid request") : base(message)
    {
    }
}