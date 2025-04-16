using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.E2ETests.Api.MotorcycleDomain.Common;
using Xunit;

namespace MatheusR.Motok.E2ETests.Api.MotorcycleDomain.CreateMotorcycle;

[CollectionDefinition(nameof(CreateMotorcycleApiTestFixture))]
public class CreateMotorcycleApiTestFixtureCollection : ICollectionFixture<CreateMotorcycleApiTestFixture>
{
    // This class is used to group the CreateMotorcycleApiTestFixture for the test collection.
    // It is empty because we are using the CategoryBaseFixture to group the tests.
    // The CategoryBaseFixture will be used to run the tests in parallel with the same category.
}

public class CreateMotorcycleApiTestFixture : MotorcycleBaseFixture
{
    public CreateMotorcycleCommand GetExampleInput() =>
       new(
           identifier: GenerateValidIdentifier(),
           year: GenerateValidYear(),
           model: GetValidMotorcycleModel(),
           licencePlate: GenerateValidLicencePlate()
       );

}
