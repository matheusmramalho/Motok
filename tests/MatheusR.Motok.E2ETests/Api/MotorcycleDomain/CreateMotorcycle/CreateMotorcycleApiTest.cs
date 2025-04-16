using FluentAssertions;
using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.CC.Models;
using System.Net;

namespace MatheusR.Motok.E2ETests.Api.MotorcycleDomain.CreateMotorcycle;

[Collection(nameof(CreateMotorcycleApiTestFixture))]
public class CreateMotorcycleApiTest
{
    private readonly CreateMotorcycleApiTestFixture _fixture;

    public CreateMotorcycleApiTest(CreateMotorcycleApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateMotorcycle))]
    [Trait("E2E/API", "Create Motorcycle Endpoints")]
    public async Task CreateMotorcycle()
    {
        // Arrange
        await _fixture.MotorcyclePersistence.ClearAllAsync();
        var command = _fixture.GetExampleInput();

        // Act
        var (response, output) = await _fixture.ApiClient
            .Post<MotorcycleOutput>(
                "/api/motorcycles",
                command
             );

        // Assert - Verifica a resposta da API
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        output.Should().NotBeNull();

        // Verifica se o output contém os dados enviados
        output.Identifier.Should().Be(command.Identifier);
        output.Year.Should().Be(command.Year);
        output.Model.Should().Be(command.Model);
        output.LicencePlate.Should().Be(command.LicencePlate);

        // Verifica no banco de dados
        var dbMotorcycle = await _fixture.MotorcyclePersistence.GetByIdAsync(output.Id);

        dbMotorcycle.Should().NotBeNull();
        dbMotorcycle!.Identifier.Should().Be(command.Identifier);
        dbMotorcycle.Year.Should().Be(command.Year);
        dbMotorcycle.Model.Should().Be(command.Model);
        dbMotorcycle.LicencePlate.Should().Be(command.LicencePlate);

        // Verifica se o output corresponde ao dado do banco
        output.Id.Should().Be(dbMotorcycle.Id);
        output.Identifier.Should().Be(dbMotorcycle.Identifier);
        output.Year.Should().Be(dbMotorcycle.Year);
        output.Model.Should().Be(dbMotorcycle.Model);
        output.LicencePlate.Should().Be(dbMotorcycle.LicencePlate);
    }

    [Fact(DisplayName = nameof(CreateMotorcycle_WithDuplicateLicencePlate_ShouldReturnConflictWithMessage))]
    [Trait("E2E/API", "Create Motorcycle Endpoints")]
    public async Task CreateMotorcycle_WithDuplicateLicencePlate_ShouldReturnConflictWithMessage()
    {
        // Arrange
        await _fixture.MotorcyclePersistence.ClearAllAsync();
        var command = _fixture.GetExampleInput();

        // Cria a primeira moto (deve funcionar)
        var (firstResponse, _) = await _fixture.ApiClient
            .Post<MotorcycleOutput>("/api/motorcycles", command);

        firstResponse.Should().NotBeNull();
        firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        // Act - Tenta criar outra moto com a mesma placa
        var (secondResponse, error) = await _fixture.ApiClient
            .Post<ApiResponseError>("/api/motorcycles", command);

        // Assert
        secondResponse.Should().NotBeNull();
        secondResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest); 

        error.Should().NotBeNull();
        error.Mensagem.Should().Contain("Licence plate already exists");
    }

}
