using RabbitMQ.Client;

namespace MatheusR.Motok.Infra.Messaging;
public class MessagingService : IMessagingService
{
    private readonly ConnectionFactory _factory;

    public MessagingService()
    {
        _factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };
    }

    public void Publish(string queue, byte[] message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: queue,
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";
        properties.MessageId = Guid.NewGuid().ToString();
        properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

        channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: message);
    }

}
