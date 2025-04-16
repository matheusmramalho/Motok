
using MatheusR.Motok.Application.Commands.Common;
using MediatR;

namespace MatheusR.Motok.Application.Commands.CreateMotorcycle;
public class CreateMotorcycleCommand : IRequest<MotorcycleOutput>
{
    public CreateMotorcycleCommand(string identifier, int year, string model, string licencePlate)
    {
        Identifier = identifier;
        Year = year;
        Model = model;
        LicencePlate = licencePlate;
    }

    public string Identifier { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string LicencePlate { get; set; }
}
