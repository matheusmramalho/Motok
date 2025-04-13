namespace MatheusR.Motok.Domain.Entities;
public class Motorcycle
{
    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string LicencePlate { get; set; }
}
