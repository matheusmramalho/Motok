using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.UpdateLicencePlate;
public class UpdateLicencePlateCommandHandler : IRequestHandler<UpdateLicencePlateCommand>
{
    private readonly IMotorcycleRepository _motorcycleRepository;

    public UpdateLicencePlateCommandHandler(IMotorcycleRepository motorcycleRepository)
    {
        _motorcycleRepository = motorcycleRepository;
    }

    public async Task<Unit> Handle(UpdateLicencePlateCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await _motorcycleRepository.GetMotorcycleById(request.Id);
        if (motorcycle == null)
            throw new MotokApplicationException("Dados inválidos");

        motorcycle.ModifyLicencePlate(request.Placa);
        await _motorcycleRepository.UpdateMotorcycle(motorcycle);

        return Unit.Value;
    }
}
