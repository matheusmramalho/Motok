using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Application.Extensions;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.CreateDelivery;
public class CreateDeliveryCommandHandler : IRequestHandler<CreateDeliveryCommand>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IImageService _imageService;

    public CreateDeliveryCommandHandler(IDeliveryRepository deliveryRepository, IImageService imageService)
    {
        _deliveryRepository = deliveryRepository;
        _imageService = imageService;
    }

    public async Task<Unit> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
    {
        var documentExiets = await _deliveryRepository.ExistsByCnpj(request.Cnpj);
        if (documentExiets)
            throw new MotokApplicationException("Dados inválidos: CNPJ existente na base");

        var licenceNumberExists = await _deliveryRepository.ExistsByCnh(request.NumeroCnh);
        if (licenceNumberExists)
            throw new MotokApplicationException("Dados inválidos: CNH existente na base");

        var delivery = request.ToEntity();

        if (request.ImagemCnh is not null)
        {
            var fileName = await _imageService.SaveImageAsync(request.ImagemCnh);
            delivery.UpdateImagePath(fileName);
        }

        await _deliveryRepository.CreateDeliveryAsync(delivery);

        return Unit.Value;
    }
}
