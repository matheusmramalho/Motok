using MatheusR.Motok.CC.Events;

namespace MatheusR.Motok.CC.Services;
public interface IMotorcycleService
{
    void ProcessMotorcycleForQueue(MotorcycleCreatedEvent motorcycleEvent);
}
