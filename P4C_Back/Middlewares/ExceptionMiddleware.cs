using System.Text.Json;
using P4C_Back.Responses.Error;

namespace P4C_Back.Exceptions
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int statusCode = 500;
            if (exception is UnauthorizedException)
            {
                statusCode = 401;
            } 
            else if (exception is ForbiddenException)
            {
                statusCode = 403;
            }
            else if (exception is ValidationException)
            {
                statusCode = 400;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            ErrorResponse errorDto = new (exception.Message, statusCode, context.Request.Host + context.Request.Path, DateTime.Now.ToString("yyyy-MM-dd h:mm:ss tt"));

            var jsonError = JsonSerializer.Serialize(errorDto, _jsonOptions);
            await context.Response.WriteAsync(jsonError);
        }
    }
}
