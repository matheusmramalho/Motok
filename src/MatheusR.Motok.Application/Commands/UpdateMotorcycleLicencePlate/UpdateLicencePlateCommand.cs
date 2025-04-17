using MediatR;

namespace MatheusR.Motok.Application.Commands.UpdateMotorcycleLicencePlate;
public class UpdateLicencePlateCommand : IRequest
{
    public UpdateLicencePlateCommand(Guid id, string placa)
    {
        Id = id;
        Placa = placa;
    }

    public Guid Id { get; set; }
    public string Placa { get; set; }

}
