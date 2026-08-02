using System.Net;
using ChatApp.Application.DTO.Common;
using ChatApp.Application.Exceptions;
using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Database;

namespace ChatApp.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,
            NotFoundException => (int)HttpStatusCode.NotFound,
            ConflictException => (int)HttpStatusCode.Conflict,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            _ => (int)HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode = statusCode;

        if (statusCode == (int)HttpStatusCode.InternalServerError)
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var errorLog = new ErrorLog
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    Source = exception.Source,
                    TargetSite = exception.TargetSite?.ToString(),
                    InnerException = exception.InnerException?.Message,
                    CreatedAt = DateTime.UtcNow
                };

                await dbContext.ErrorLogs.AddAsync(errorLog);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var responseMessage = statusCode == (int)HttpStatusCode.InternalServerError
            ? "Internal Server Error"
            : exception.Message;

        var response = ApiResponse<object>.FailureResponse(statusCode, responseMessage);
        await context.Response.WriteAsJsonAsync(response);
    }
}
