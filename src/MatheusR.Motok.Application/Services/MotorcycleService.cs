using MatheusR.Motok.CC.Services;
using System.Text.Json;
using System.Text;
using MatheusR.Motok.Infra.Messaging;
using MatheusR.Motok.CC.Events;

namespace MatheusR.Motok.Application.Services;
public class MotorcycleService : IMotorcycleService
{
    private readonly IMessagingService _messagingService;
    private const string queueName = "motorcycles_created";

    public MotorcycleService(IMessagingService messageBusService)
    {
        _messagingService = messageBusService;
    }

    public void ProcessMotorcycleForQueue(MotorcycleCreatedEvent motorcycleEvent)
    {
        var motorcycleJson = JsonSerializer.Serialize(motorcycleEvent);

        var motorcycleInBytes = Encoding.UTF8.GetBytes(motorcycleJson);

        _messagingService.Publish(queueName, motorcycleInBytes);
    }
}
