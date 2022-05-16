using Gateways.Common.Models;
using Gateways.Common.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Gateways.Common.Filters;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var result = new ObjectResult(null)
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            ContentTypes = { MediaTypeNames.Application.Json }
        };

        var logger = (ILogger?)context.HttpContext.RequestServices.GetService(typeof(ILogger));
        var error = context.Exception;
        if (error != null)
        {
            var exceptionCode = ExceptionsCodes.FirstOrDefault(e => e.Item1.IsInstanceOfType(error));
            result.StatusCode = exceptionCode?.Item2 ?? StatusCodes.Status500InternalServerError;
            result.Value = new Response
            {
                Data = null,
                StatusCode = (int)result.StatusCode,
                Message = exceptionCode == null ? "Internal Server Error" : error.Message,
            };
            logger?.LogError(error, error.Message);
            context.Result = result;
            context.ExceptionHandled = true;
        }
    }

    private static IEnumerable<Tuple<Type, int, bool>> ExceptionsCodes => new Tuple<Type, int, bool>[]
    {
            new Tuple<Type, int, bool>(typeof(BadRequestError), StatusCodes.Status400BadRequest, false),
            new Tuple<Type, int, bool>(typeof(ForbiddenError), StatusCodes.Status401Unauthorized, false),
            new Tuple<Type, int, bool>(typeof(NotFoundError), StatusCodes.Status404NotFound, false),
            new Tuple<Type, int, bool>(typeof(ServerError), StatusCodes.Status500InternalServerError, true),
    };
}
