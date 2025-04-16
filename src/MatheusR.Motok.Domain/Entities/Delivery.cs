using MatheusR.Motok.Domain.Entities.Base;
using MatheusR.Motok.Domain.Enums;

namespace MatheusR.Motok.Domain.Entities;
public class Delivery : Entity
{
    protected Delivery() { }

    public Delivery(
        string name,
        string cnpj,
        DateOnly birthDate,
        string licenceNumber,
        LicenteType licenceType,
        string licenteImagePath,
        List<Rent> rents)
    {
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
        LicenceNumber = licenceNumber;
        LicenceType = licenceType;
        LicenteImagePath = licenteImagePath;
        Rents = rents;
    }

    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public string LicenceNumber { get; private set; }
    public LicenteType LicenceType { get; private set; }
    public string LicenteImagePath { get; private set; }
    public List<Rent> Rents { get; private set; }

    public void UpdateImagePath(string imagePath)
    {
        LicenteImagePath = imagePath;
    }

    public void AddRent(Rent rent)
    {
        Rents.Add(rent);
    }

}
