using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.E2ETests.Api.MotorcycleDomain.Common;
using Xunit;

namespace MatheusR.Motok.E2ETests.Api.MotorcycleDomain.GetMotorcycles;

[CollectionDefinition(nameof(GetMotorcyclesApiTestFixture))]
public class GetMotorcyclesApiTestFixtureCollection : ICollectionFixture<GetMotorcyclesApiTestFixture>
{
    // This class is used to group the CreateMotorcycleApiTestFixture for the test collection.
    // It is empty because we are using the CategoryBaseFixture to group the tests.
    // The CategoryBaseFixture will be used to run the tests in parallel with the same category.
}

public class GetMotorcyclesApiTestFixture : MotorcycleBaseFixture
{
    public CreateMotorcycleCommand GetExampleInput() =>
       new(
           identifier: GenerateValidIdentifier(),
           year: GenerateValidYear(),
           model: GetValidMotorcycleModel(),
           licencePlate: GenerateValidLicencePlate()
       );
}
