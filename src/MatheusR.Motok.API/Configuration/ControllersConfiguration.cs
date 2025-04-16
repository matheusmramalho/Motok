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
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseDocumentation(this WebApplication app)
    {

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
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

        //var userId = Guid.NewGuid();

        //await contextIdentity.Users.AddAsync(new IdentityUser
        //{
        //    Id = userId.ToString(),
        //    UserName = "teste@teste.com",
        //    NormalizedUserName = "TESTE@TESTE.COM",
        //    Email = "teste@teste.com",
        //    NormalizedEmail = "TESTE@TESTE.COM",
        //    AccessFailedCount = 0,
        //    LockoutEnabled = false,
        //    PasswordHash = "AQAAAAIAAYagAAAAEEdWhqiCwW/jZz0hEM7aNjok7IxniahnxKxxO5zsx2TvWs4ht1FUDnYofR8JKsA5UA==",
        //    TwoFactorEnabled = false,
        //    ConcurrencyStamp = Guid.NewGuid().ToString(),
        //    EmailConfirmed = true,
        //    SecurityStamp = Guid.NewGuid().ToString()
        //});

        //await contextIdentity.UserClaims.AddAsync(new IdentityUserClaim<string>
        //{
        //    UserId = userId.ToString(),
        //    ClaimType = "Customers",
        //    ClaimValue = "Write,Remove"
        //});

        //await contextIdentity.SaveChangesAsync();

        //if (context.Customers.Any())
        //    return;

        //var customer = new Customer(
        //    Guid.NewGuid(),
        //    "Eduardo Pires",
        //    "contato@eduardopires.net.br",
        //    new DateTime(1982, 04, 24));

        //await context.Customers.AddAsync(customer);

        //await context.SaveChangesAsync();

        //var customerEvent = new CustomerRegisteredEvent(customer.Id,
        //                                                customer.Name,
        //                                                customer.Email,
        //                                                customer.BirthDate);

        //var serializedData = JsonConvert.SerializeObject(customerEvent);

        //await contextStore.StoredEvent.AddAsync(new StoredEvent(customerEvent, serializedData, "teste@teste.com"));

        //await contextStore.SaveChangesAsync();
    }


}
