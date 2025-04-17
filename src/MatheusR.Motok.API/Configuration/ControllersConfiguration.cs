using MatheusR.Motok.API.Filters;
using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Insira apenas o token JWT.",
                Type = SecuritySchemeType.Http
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static WebApplication UseDocumentation(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
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
        DbMigrationHelper.EnsureSeedData(app).Wait();

        return app;
    }
}
