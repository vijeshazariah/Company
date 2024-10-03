using Company.Middleware;
using CorrelationId;
using System.Net;

public class ExceptionHandlingMiddleware
{

    private const string CorrelationIdHeader = "X-Correlation-ID";
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
 

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Check if the client sent a correlation ID, otherwise generate a new one
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[CorrelationIdHeader] = correlationId;
            }

            // Add correlation ID to the response headers
            context.Response.Headers[CorrelationIdHeader] = correlationId;

            // Create a logging scope with the correlation ID
            using (_logger.BeginScope("{CorrelationId}", correlationId))
            {
                _logger.LogInformation("Handling request with Correlation ID: {CorrelationId}", correlationId);
                await _next(context);  // Continue processing the request
            }
        }
        catch (BusinessValidationException ex)
        {
            _logger.LogError(ex, "Business validation error");
            await HandleBusinessValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            // Catch the exception and handle it
            await HandleExceptionAsync(context, ex);
        }
    }
    private Task HandleBusinessValidationExceptionAsync(HttpContext context, BusinessValidationException ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message,
        };

        // Return error response in JSON format
         return context.Response.WriteAsJsonAsync(errorResponse);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception details
        _logger.LogError(exception, "An unhandled exception occurred while processing the request.");

        // Set the status code and return a generic error message
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An internal server error occurred. Please try again later."
        };

        // Return error response in JSON format
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
