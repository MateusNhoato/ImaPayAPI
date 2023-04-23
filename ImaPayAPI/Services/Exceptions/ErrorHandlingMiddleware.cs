using ImaPayAPI.Services.Exceptions;
using Newtonsoft.Json;
using Serilog;
using System.Net;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        Log.Error(exception, "Erro na aplicação");

        var code = HttpStatusCode.InternalServerError;

        if (exception is Exception) code = HttpStatusCode.BadRequest;
        else if (exception is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;
        else if (exception is NotFoundException) code = HttpStatusCode.NotFound;

        var result = JsonConvert.SerializeObject(new { error = exception.Message });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}