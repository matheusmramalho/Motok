using Bogus;
using MatheusR.Motok.E2ETests.Api.MotorcycleDomain.Common;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.E2ETests.Base;
public class BaseFixture
{
    protected Faker Faker { get; set; }
    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }
    public HttpClient HttpClient { get; set; }
    public ApiClient ApiClient { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(HttpClient);
    }

    public AppDbContext CreateDbContext()
    {
        var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("e2e-tests-db").Options);
        return context;
    }
}
