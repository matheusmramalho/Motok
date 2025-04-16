using MatheusR.Motok.SimpleMediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MatheusR.Motok.SimpleMediator.Implementations;
public class Mediator : IMediator
{
    private readonly IServiceProvider _provider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _provider = serviceProvider;
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _provider.GetService(handlerType);

        if (handler == null)
            throw new InvalidOperationException($"Handler for {request.GetType().Name} not found.");

        return await (Task<TResponse>)handlerType
            .GetMethod("Handle")!
            .Invoke(handler, new object[] { request, cancellationToken })!;
    }
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
    {
        var handlerType = typeof(INotificationHandler<>).MakeGenericType(notification.GetType());
        var handlers = _provider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            await (Task)handlerType
                  .GetMethod("Handle")!
                  .Invoke(handler, new object[] { notification, cancellationToken })!;
        }
    }
}
