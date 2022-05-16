namespace Gateways.Common.Errors;

public class ForbiddenError : Exception
{
    public ForbiddenError(string message = "Unauthorized") : base(message)
    {
    }
}