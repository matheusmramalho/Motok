using MatheusR.Motok.API.Filters;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.API.Configuration;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(options
            => options.Filters.Add(typeof(ApiGlobalExceptionHandler))
        );

        services.AddDocumentation();

        return services;
    }

    private static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        //services.AddSwaggerGen(opt => opt.EnableAnnotations());
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication UseDocumentation(this WebApplication app)
    {

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        return app;
    }

    public static WebApplication UseMigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        if (app.Environment.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            EnsureSeedData(dbContext);
        }

        return app;
    }

    private static void EnsureSeedData(AppDbContext context)
    {
        // Verificar se ja existe os dados

        //if (contextIdentity.Users.Any())
        //    return;

        // Caso não exista adiciona os dados de usuarios
    }
}
