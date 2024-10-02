using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Common.Exceptions
{
    public class HttpErrorHandling(
        RequestDelegate next,
        ILogger<HttpErrorHandling> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<HttpErrorHandling> _logger = logger;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(new { StatusCode = context.Response.StatusCode, Errors = validationException.GetBaseException() });
                    _logger.LogError(validationException.Message);
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsJsonAsync(new { StatusCode = context.Response.StatusCode, Message = "Internal Server Error." });
                    _logger.LogError(exception.Message);
                    break;
            }
        }
    }
}
