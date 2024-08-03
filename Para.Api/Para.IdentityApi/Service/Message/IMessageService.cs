using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public interface IMessageService
{
    void ProduceMessage(NotificationTemplate template);
    void Consumer();
}