using System.Text.Json;
using Serilog;

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
            Log.Fatal(                        
                $"Path={context.Request.Path} || " +                      
                $"Method={context.Request.Method} || " +
                $"Exception={ex.Message}"
            );

            context.Response.StatusCode = 500;
            context.Request.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Server Error"));
        }
       
    }
    
}