using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Application.Commands.Common;
public class MotorcycleOutput
{
    public MotorcycleOutput(Guid id, string identifier, int year, string model, string licencePlate)
    {
        Id = id;
        Identifier = identifier;
        Year = year;
        Model = model;
        LicencePlate = licencePlate;
    }

    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string LicencePlate { get; set; }

    public static MotorcycleOutput FromEntity(Motorcycle motorcycle)
    {
        return new MotorcycleOutput(
            id: motorcycle.Id,
            identifier: motorcycle.Identifier,
            year: motorcycle.Year,
            model: motorcycle.Model,
            licencePlate: motorcycle.LicencePlate
        );
    }
}
