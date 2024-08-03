using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Para.IdentityApi.Schema;
using Para.IdentityApi.Service;

namespace Para.IdentityApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IMessageService messageService;

    public NotificationsController(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    [HttpPost("SetRecurringJob")]
    public string Recurring()
    {
        RecurringJob.AddOrUpdate("notificationjob", () => messageService.Consumer(), "*/2 * * * *");
        return "notificationjob";
    }

    [HttpPost("SendNotification")]
    public string SendNotification(NotificationTemplate template)
    {
        messageService.ProduceMessage(template);
        return "1";
    }
}