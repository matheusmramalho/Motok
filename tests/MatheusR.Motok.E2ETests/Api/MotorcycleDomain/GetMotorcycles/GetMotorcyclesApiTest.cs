using FluentAssertions;
using MatheusR.Motok.Application.Commands.Common;
using System.Net;

namespace MatheusR.Motok.E2ETests.Api.MotorcycleDomain.GetMotorcycles;

[Collection(nameof(GetMotorcyclesApiTestFixture))]
public class GetMotorcyclesApiTest
{

    private readonly GetMotorcyclesApiTestFixture _fixture;

    public GetMotorcyclesApiTest(GetMotorcyclesApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetMotorcycle_WithId_ReturnsSpecificMotorcycle))]
    [Trait("E2E/API", "Get Motorcycles Endpoints")]
    public async Task GetMotorcycle_WithId_ReturnsSpecificMotorcycle()
    {
        // Arrange
        await _fixture.MotorcyclePersistence.ClearAllAsync();
        var exampleList = _fixture.GetExampleCategoriesList(20);
        await _fixture.MotorcyclePersistence.AddListAsync(exampleList);
        var exampleMotorcycle = exampleList[10];

        // Act
        var (response, output) = await _fixture.ApiClient.Get<IEnumerable<MotorcycleOutput>>(
           $"/api/motorcycles?licencePlate={exampleMotorcycle.LicencePlate}");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.Should().NotBeNull();
        output.First().Id.Should().Be(exampleMotorcycle.Id);
        output.First().Identifier.Should().Be(exampleMotorcycle.Identifier);
        output.First().Year.Should().Be(exampleMotorcycle.Year);
        output.First().Model.Should().Be(exampleMotorcycle.Model);
        output.First().LicencePlate.Should().Be(exampleMotorcycle.LicencePlate);
    }

    [Fact(DisplayName = nameof(GetMotorcycle_WithoutId_ReturnsAllMotorcycles))]
    [Trait("E2E/API", "Get Motorcycles Endpoints")]
    public async Task GetMotorcycle_WithoutId_ReturnsAllMotorcycles()
    {
        // Arrange
        await _fixture.MotorcyclePersistence.ClearAllAsync();
        var exampleList = _fixture.GetExampleCategoriesList(20);
        await _fixture.MotorcyclePersistence.AddListAsync(exampleList);

        // Act
        var (response, output) = await _fixture.ApiClient.Get<List<MotorcycleOutput>>(
            "/api/motorcycles");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.Should().NotBeNull();
        output.Should().HaveCount(exampleList.Count);

        // Verifica se o primeiro item da lista corresponde ao primeiro item adicionado
        var firstExample = exampleList[0];
        var firstOutput = output[0];
        firstOutput.Id.Should().Be(firstExample.Id);
        firstOutput.Identifier.Should().Be(firstExample.Identifier);
        firstOutput.Year.Should().Be(firstExample.Year);
        firstOutput.Model.Should().Be(firstExample.Model);
        firstOutput.LicencePlate.Should().Be(firstExample.LicencePlate);
    }

    [Fact(DisplayName = nameof(GetMotorcycle_WithInvalidId_ReturnsNotFound))]
    [Trait("E2E/API", "Get Motorcycles Endpoints")]
    public async Task GetMotorcycle_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        await _fixture.MotorcyclePersistence.ClearAllAsync();
        var invalidId = Guid.NewGuid();

        // Act
        var (response, _) = await _fixture.ApiClient.Get<MotorcycleOutput>(
            $"/api/motorcycles/{invalidId}");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
