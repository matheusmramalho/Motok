using MatheusR.Motok.API.Configuration;
using MatheusR.Motok.Application.Extensions;
using MatheusR.Motok.Infra.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Controllers
builder.Services
    .AddAndConfigureControllers()
    .AddUseCases()
    .AddInfrastructure(configuration)
    .AddApplicationServices()
    .AddMessagingConsumers();

var app = builder.Build();
app.UseDocumentation()
    .UseMigrateDatabase();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
public partial class Program { }