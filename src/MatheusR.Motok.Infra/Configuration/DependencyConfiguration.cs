using MatheusR.Motok.CC.Repositories;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Infra.Database;
using MatheusR.Motok.Infra.Messaging;
using MatheusR.Motok.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MatheusR.Motok.Infra.Configuration;
public static class DependencyConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddDbConnection(configuration);
        services.AddMessaging();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
        services.AddTransient<IDeliveryRepository, DeliveryRepository>();
        services.AddTransient<IRentRepository, RentRepository>();

        return services;
    }

    private static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddScoped<IMessagingService, MessagingService>();

        return services;
    }

}
