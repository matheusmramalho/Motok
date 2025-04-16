using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetMotorcycles;
public class GetMotorcyclesCommandHandler : IRequestHandler<GetMotorcyclesCommand, IEnumerable<MotorcycleOutput>>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public GetMotorcyclesCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<IEnumerable<MotorcycleOutput>> Handle(GetMotorcyclesCommand request, CancellationToken cancellationToken)
    {
        if (request.LicencePlate is null)
        {
            var motorcycles = await _motorcycleRepository.GetAllMotorcycles();
            return motorcycles.Select(MotorcycleOutput.FromEntity);
        }

        var motorcycle = await _motorcycleRepository.GetMotorcycleByLicencePlate(request.LicencePlate);

        if (motorcycle == null)
            return new List<MotorcycleOutput>();

        return new List<MotorcycleOutput> { MotorcycleOutput.FromEntity(motorcycle) };
    }
}
