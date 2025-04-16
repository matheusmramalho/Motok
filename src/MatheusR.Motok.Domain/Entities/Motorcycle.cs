using MatheusR.Motok.Domain.Entities.Base;

namespace MatheusR.Motok.Domain.Entities;
public class Motorcycle : Entity
{
    public string Identifier { get; private set; }
    public int Year { get; private set; }
    public string Model { get; private set; }
    public string LicencePlate { get; private set; }
    public List<Rent> Rents { get; private set; }

    protected Motorcycle() { }

    public Motorcycle(
        string identifier,
        int year,
        string model,
        string licencePlate)
    {
        Identifier = identifier;
        Year = year;
        Model = model;
        LicencePlate = licencePlate;
        Rents = new List<Rent>();

        Validate();
    }

    public void Validate()
    {
        if (String.IsNullOrWhiteSpace(Identifier))
            throw new ArgumentException("Identifier cannot be null or empty.");

        if (Year <= 1885)
            throw new ArgumentException("Invalid motorcycle year.");

        if (String.IsNullOrWhiteSpace(Model))
            throw new ArgumentException("Model cannot be null or empty.");

        if (String.IsNullOrWhiteSpace(LicencePlate))
            throw new ArgumentException("LicencePlate cannot be null or empty.");
    }

    public void ModifyLicencePlate(string licencePlate)
    {
        LicencePlate = licencePlate;
        Validate();
    }
}
