using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Para.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{

    [HttpGet("FireAndForget")]
    public string FireAndForget()
    {
        var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
        return jobId;
    }
    
    [HttpGet("Delayed")]
    public string Delayed()
    {
        var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromSeconds(27));
        return jobId;
    }
    
    [HttpGet("Recurring")]
    public string Recurring()
    {
        RecurringJob.AddOrUpdate("nntdr", () => Console.WriteLine("Recurring!"), Cron.Daily);
        RecurringJob.AddOrUpdate("agust", () => Console.WriteLine("Recurring!"), "15 14 1 * *");
    
        return "nntdr";
    }
    
    [HttpGet("Continuations")]
    public string Continuations()
    {
        var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Delayed!"), TimeSpan.FromSeconds(50));
        BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation!"));
        return jobId;
    }
}