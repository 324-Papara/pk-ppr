using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public interface INotificationService
{
    public void SendEmail(NotificationTemplate template);
}