using System.Text.Json;

namespace Para.Api.Middleware;


public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // before controller invoke
            await next.Invoke(context);
            // after controller invoke
        }
        catch (Exception ex)
        {
            // log

            context.Response.StatusCode = 500;
            context.Request.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Server Error"));
        }
       
    }
    
}