namespace MatheusR.Motok.SimpleMediator.Interfaces;
public interface INotificationHandler<TNotification> where TNotification : INotification
{
    Task Handle(TNotification notification, CancellationToken cancellationToken);
}
