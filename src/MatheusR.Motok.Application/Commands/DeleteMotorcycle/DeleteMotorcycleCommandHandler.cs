using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.DeleteMotorcycle;
public class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleCommand>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public DeleteMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<Unit> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetMotorcycleById(request.Id);
        if (motorcycle == null)
            throw new MotokApplicationException("Dados inválidos");

        await _motorcycleRepository.DeleteMotorcycle(motorcycle);

        return Unit.Value;
    }
}
