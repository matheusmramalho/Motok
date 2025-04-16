using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Application.Extensions;
using MatheusR.Motok.CC.Events;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.CreateMotorcycle;
public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, MotorcycleOutput>
{
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IMotorcycleService _motorcycleService;

    public CreateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository, IMotorcycleService motorcycleService)
    {
        _motorcycleRepository = motorcycleRepository;
        _motorcycleService = motorcycleService;
    }

    public async Task<MotorcycleOutput> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var licencePlateExists = await _motorcycleRepository.LicencePlateExists(request.LicencePlate);
        if (licencePlateExists)
            throw new MotokApplicationException("Licence plate already exists");

        var motorcycle = request.ToEntity();
        await _motorcycleRepository.CreateMotorcycle(motorcycle);

        _motorcycleService.ProcessMotorcycleForQueue(new MotorcycleCreatedEvent(motorcycle.Id));

        return MotorcycleOutput.FromEntity(motorcycle);
    }
}
