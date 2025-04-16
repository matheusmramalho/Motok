using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.Application.Consumers;
using MatheusR.Motok.Application.Services;
using MatheusR.Motok.CC.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MatheusR.Motok.Application.Extensions;
public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateMotorcycleCommand));

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMotorcycleService, MotorcycleService>();

        return services;
    }

    public static IServiceCollection AddMessagingConsumers(this IServiceCollection services)
    {
        services.AddHostedService<MotorcycleCreatedConsumer>();

        return services;
    }
}
