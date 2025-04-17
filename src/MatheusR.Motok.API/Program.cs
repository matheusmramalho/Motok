using MatheusR.Motok.API.Configuration;
using MatheusR.Motok.Application.Extensions;
using MatheusR.Motok.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Services configuration
builder.Services
    .AddAndConfigureControllers()
    .AddUseCases()
    .AddInfrastructure(configuration)
    .AddApplicationRepositories()
    .AddHttpContextAccessor()
    .AddApplicationServices()
    .AddMessagingConsumers()
    .AddJwtConfiguration(configuration);

var app = builder.Build();

// Documentation and migrations
app.UseDocumentation()
    .UseMigrateDatabase();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program { }