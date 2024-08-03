using System.Text;
using Newtonsoft.Json;
using Para.IdentityApi.Schema;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Para.IdentityApi.Service;

public class MessageService : IMessageService
{
    private readonly string QueueName = "notificationqueue";
    private readonly INotificationService notificationService;
    
    public MessageService(INotificationService notificationService)
    {
        this.notificationService = notificationService;
    }

    public void ProduceMessage(NotificationTemplate template)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            string message = JsonConvert.SerializeObject(template);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
            Thread.Sleep(3000);
        }
    }

    public void Consumer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (IConnection connection = factory.CreateConnection())
        using (IModel channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                NotificationTemplate item = JsonConvert.DeserializeObject<NotificationTemplate>(message);
                notificationService.SendEmail(item);
                Thread.Sleep(1000);
            };
            channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
        }
        
    }
}