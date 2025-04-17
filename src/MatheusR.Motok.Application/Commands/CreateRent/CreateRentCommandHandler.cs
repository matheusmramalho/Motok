using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Application.Extensions;
using MatheusR.Motok.Domain.Enums;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.CreateRent;
public class CreateRentCommandHandler : IRequestHandler<CreateRentCommand>
{
    private readonly IRentRepository _rentRepository;
    private readonly IMotorcycleRepository _motorcycleRepository;
    private readonly IDeliveryRepository _deliveryRepository;

    public CreateRentCommandHandler(IRentRepository rentRepository,
                                    IMotorcycleRepository motorcycleRepository,
                                    IDeliveryRepository deliveryRepository)
    {
        _rentRepository = rentRepository;
        _motorcycleRepository = motorcycleRepository;
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Unit> Handle(CreateRentCommand request, CancellationToken cancellationToken)
    {
        if (request.DataPrevisaoTermino.Date <= DateTime.UtcNow.AddDays(1).Date)
            throw new MotokApplicationException("Data de previsão de término deve ser pelo menos no dia seguinte");

        var delivery = await _deliveryRepository.GetDeliveryById(request.EntregadorId);
        if (delivery == null)
            throw new MotokNotFoundException("Entregador não encontrado");

        if (delivery.LicenceType != LicenteType.A)
            throw new MotokApplicationException("Apenas motoristas com carteira Tipo A podem alugar motos");

        var motorcycle = await _motorcycleRepository.GetMotorcycleById(request.MotoId);
        if (motorcycle == null)
            throw new MotokNotFoundException("Moto não encontrada");

        var rent = request.ToEntity(delivery, motorcycle);
        rent.CalculateExpectedPrice();

        await _rentRepository.CreateRent(rent);

        return Unit.Value;
    }
}
