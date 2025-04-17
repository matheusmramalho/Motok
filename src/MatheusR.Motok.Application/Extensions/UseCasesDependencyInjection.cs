using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.Application.Consumers;
using MatheusR.Motok.Application.Repositories;
using MatheusR.Motok.Application.Services;
using MatheusR.Motok.CC.Repositories;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.CC.UserContext;
using MatheusR.Motok.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MatheusR.Motok.Application.Extensions;
public static class UseCasesDependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddMediatR(typeof(CreateMotorcycleCommand));

        return services;
    }

    public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
    {
        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMotorcycleService, MotorcycleService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

    public static IServiceCollection AddMessagingConsumers(this IServiceCollection services)
    {
        services.AddHostedService<MotorcycleCreatedConsumer>();

        return services;
    }
}
