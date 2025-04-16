using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Domain.OtherTables;
using Microsoft.Extensions.Logging;
using MatheusR.Motok.CC.Events;

namespace MatheusR.Motok.Application.Consumers;
public class MotorcycleCreatedConsumer : BackgroundService
{
    private const string MOTORCYCLES_CREATED_QUEUE = "motorcycles_created";
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MotorcycleCreatedConsumer> _logger;

    public MotorcycleCreatedConsumer(IServiceProvider serviceProvider, ILogger<MotorcycleCreatedConsumer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;

        try
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _logger.LogInformation("Creating RabbitMQ connection...");

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: MOTORCYCLES_CREATED_QUEUE,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _logger.LogInformation("MotorcycleCreatedConsumer initialized successfully");
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to initialize MotorcycleCreatedConsumer");
            throw;
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting MotorcycleCreatedConsumer execution...");

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, eventArgs) =>
        {
            try
            {
                _logger.LogDebug("Message received from queue {QueueName}", MOTORCYCLES_CREATED_QUEUE);

                var motorcycleCreatedBytes = eventArgs.Body.ToArray();
                var motorcycleCreatedJson = Encoding.UTF8.GetString(motorcycleCreatedBytes);

                _logger.LogDebug("Raw message content: {MessageContent}", motorcycleCreatedJson);

                var motorcycleCreatedEvent = JsonSerializer.Deserialize<MotorcycleCreatedEvent>(motorcycleCreatedJson);

                await ProcessMotorcycle(motorcycleCreatedEvent!.MotorcycleId);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error in message processing pipeline");

                throw;
            }
        };

        _channel.BasicConsume(MOTORCYCLES_CREATED_QUEUE, false, consumer);

        return Task.CompletedTask;
    }

    private async Task ProcessMotorcycle(Guid id)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            _logger.LogDebug("Creating scope for processing motorcycle ID {MotorcycleId}", id);

            var motorcycleRepository = scope.ServiceProvider.GetRequiredService<IMotorcycleRepository>();

            _logger.LogDebug("Fetching motorcycle with ID {MotorcycleId} from repository", id);

            var motorcycle = await motorcycleRepository.GetMotorcycleById(id);

            if (motorcycle == null)
            {
                _logger.LogWarning("Motorcycle with ID {MotorcycleId} not found in repository", id);
                return;
            }

            _logger.LogDebug("Motorcycle found: ID {MotorcycleId}, Year {Year}", motorcycle.Id, motorcycle.Year);

            if (motorcycle.Year == 2024)
            {
                _logger.LogInformation("Processing 2024 motorcycle with ID {MotorcycleId}", id);

                var motorcycle2024 = new Motorcycle2024(motorcycle);
                await motorcycleRepository.SaveMotorcycle2024(motorcycle2024);

                _logger.LogInformation("Successfully saved Motorcycle2024 with ID {MotorcycleId}", motorcycle2024.Id);
            }
        }
    }
}
