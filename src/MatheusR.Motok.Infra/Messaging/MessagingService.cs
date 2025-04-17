using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace MatheusR.Motok.Infra.Messaging;
public class MessagingService : IMessagingService
{
    private readonly ConnectionFactory _factory;

    public MessagingService(IConfiguration configuration)
    {
        _factory = new ConnectionFactory()
        {
            HostName = configuration["RabbitMq:HostName"],
            UserName = configuration["RabbitMq:Username"],
            Password = configuration["RabbitMq:Password"]
        };
    }

    public void Publish(string queue, byte[] message)
    {
        // TODO: Lembrar de mudar a chave "durable" caso for feita ambiente de produção
        
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
