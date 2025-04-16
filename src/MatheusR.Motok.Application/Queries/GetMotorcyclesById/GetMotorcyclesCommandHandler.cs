using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetMotorcycles;
public class GetMotorcycleByIdCommandHandler : IRequestHandler<GetMotorcycleByIdCommand, MotorcycleOutput>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public GetMotorcycleByIdCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<MotorcycleOutput> Handle(GetMotorcycleByIdCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetMotorcycleById(request.Id);

        if (motorcycle == null)
            throw new MotokNotFoundException("Moto não encontrada");

        return MotorcycleOutput.FromEntity(motorcycle);
    }
}
