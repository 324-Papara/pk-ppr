using System.Text.Json;

namespace Para.Api.Middleware;


public class HeartbeatMiddleware
{
    private readonly RequestDelegate next;

    public HeartbeatMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/heartbeat"))
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(JsonSerializer.Serialize("Hello world !"));
            return;
        }

        await next.Invoke(context);
    }
    
}