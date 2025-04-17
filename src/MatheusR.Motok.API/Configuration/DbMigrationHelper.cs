using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.API.Configuration;

public static class DbMigrationHelper
{
    public static async Task EnsureSeedData(WebApplication serviceScope)
    {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        await EnsureSeedData(services);
    }


    public static async Task EnsureSeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        if (env.IsDevelopment())
        {
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.Database.Migrate();

            await EnsureSeedUsersAndRoles(dbContext, authService, logger);
        }
    }

    public static async Task EnsureSeedUsersAndRoles(AppDbContext context, IAuthService authService, ILogger<Program> logger)
    {
        try
        {
            // Verificar se ja existe usuários cadastrados
            if (context.Users.Any())
                return;

            // Caso não exista, adiciona os usuário
            var adminRole = new Role("ADMIN");
            var deliveryRole = new Role("DELIVERY");

            await context.Roles.AddAsync(adminRole);
            await context.Roles.AddAsync(deliveryRole);

            await context.SaveChangesAsync();

            var adminUserInput = new RegisterUserInputModel("admin user", "admin", "1234567890", "ADMIN");
            var deliveryUserInput = new RegisterUserInputModel("delivery user", "delivery", "1234567890", "DELIVERY");

            await authService.RegisterAsync(adminUserInput);
            await authService.RegisterAsync(deliveryUserInput);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao criar os dados iniciais");
            throw;
        }
    }
}
