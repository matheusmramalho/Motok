using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.E2ETests.Base;
using System.Linq;

namespace MatheusR.Motok.E2ETests.Api.MotorcycleDomain.Common;
public class MotorcycleBaseFixture : BaseFixture
{
    public MotorcyclePersistence MotorcyclePersistence;

    public MotorcycleBaseFixture() : base()
    {
        MotorcyclePersistence = new MotorcyclePersistence(CreateDbContext());
    }

    public string GenerateValidIdentifier() =>
          Faker.Random.AlphaNumeric(10); // ou qualquer lógica de identificador que você preferir

    public int GenerateValidYear() =>
        Faker.Date.Past(10).Year; // ou Faker.Random.Int(1900, DateTime.Now.Year)

    public string GetValidMotorcycleModel() =>
        Faker.Vehicle.Model(); // Já existe no Bogus para modelos de veículos

    public string GenerateValidLicencePlate()
    {
        // Exemplo de placa no formato brasileiro (AAA-9999)
        var letters = Faker.Random.String(3, 'A', 'Z');
        var numbers = Faker.Random.Int(1000, 9999).ToString();
        return $"{letters}-{numbers}";

        // Ou para o novo formato (AAA9A99)
        // var part1 = Faker.Random.String(3, 'A', 'Z');
        // var part2 = Faker.Random.Int(0, 9);
        // var part3 = Faker.Random.String(1, 'A', 'Z');
        // var part4 = Faker.Random.Int(10, 99);
        // return $"{part1}{part2}{part3}{part4}";
    }

    public int GetInvalidMotorcycleYear() =>
        1885;

    public Motorcycle GetExampleMotorcycle()
    => new(
        identifier: GenerateValidIdentifier(),
        year: GenerateValidYear(),
        model: GetValidMotorcycleModel(),
        licencePlate: GenerateValidLicencePlate()
    );

    public List<Motorcycle> GetExampleCategoriesList(int listLength = 15)
        => Enumerable.Range(1, listLength).Select(
            _ => new Motorcycle(
                GenerateValidIdentifier(),
                GenerateValidYear(),
                GetValidMotorcycleModel(),
                GenerateValidLicencePlate()
            )
        ).ToList();
}
