using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Infra.Database;
using MatheusR.Motok.Infra.Repositories;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MatheusR.Motok.E2ETests.Base;
public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbOptions = services.FirstOrDefault(x => x.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (dbOptions is not null)
            {
                services.Remove(dbOptions);
            }

            // DbContext
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("e2e-tests-db"));

            // Repositories
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            
            services.AddMediatR(typeof(CreateMotorcycleCommand));

            // UseCases SimpleMediator
            //services.AddSingleton<IMediator, Mediator>();
            //services.AddTransient<IRequestHandler<CreateMotorcycleCommand, string>, CreateMotorcycleCommandHandler>();
        });

        base.ConfigureWebHost(builder);
    }
}
