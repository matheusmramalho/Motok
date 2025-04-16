using MatheusR.Motok.Application.Commands.Common;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetMotorcycles;
public class GetMotorcyclesCommand : IRequest<IEnumerable<MotorcycleOutput>>
{
    public string? LicencePlate { get; private set; }
    public GetMotorcyclesCommand(string? licencePlate) => LicencePlate = licencePlate;
}
