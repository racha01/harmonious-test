using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Common.Exceptions
{
    public sealed class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var result = new ProblemDetails();

            switch (exception)
            {
                case HttpException httpException:
                    result = new ProblemDetails
                    {
                        Status = (int)httpException.StatusCode,
                        Detail = httpException.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };
                    httpContext.Response.StatusCode = (int)httpException.StatusCode;
                    _logger.LogError(httpException, $"Exception occurred: {httpException.Message}");
                    break;

                default:
                    result = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = exception.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(exception, $"Exception occurred: {exception.Message}");
                    break;
            }
            
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);

            return true;
        }
    }

}
