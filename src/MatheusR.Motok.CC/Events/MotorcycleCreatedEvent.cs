namespace MatheusR.Motok.CC.Events;
public class MotorcycleCreatedEvent
{
    public Guid MotorcycleId { get; set; }

    public MotorcycleCreatedEvent(Guid motorcycleId)
    {
        MotorcycleId = motorcycleId;
    }
}
