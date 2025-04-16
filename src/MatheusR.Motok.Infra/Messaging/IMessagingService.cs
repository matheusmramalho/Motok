namespace MatheusR.Motok.Infra.Messaging;
public interface IMessagingService
{
    void Publish(string queue, byte[] message);
}
