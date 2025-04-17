using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.UpdateDeliveryLicenceImage;
public class UpdateDeliveryLicenceImageCommandHandler : IRequestHandler<UpdateDeliveryLicenceImageCommand>
{
    private readonly IImageService _imageService;
    private readonly IDeliveryRepository _deliveryRepository;

    public UpdateDeliveryLicenceImageCommandHandler(IImageService imageService, IDeliveryRepository deliveryRepository)
    {
        _imageService = imageService;
        _deliveryRepository = deliveryRepository;
    }

    public async Task<Unit> Handle(UpdateDeliveryLicenceImageCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryRepository.GetDeliveryById(request.Id);
        if (delivery is null)
            throw new MotokApplicationException("Entregador não encontrado");

        if (delivery.LicenteImagePath is not null)
        {
            _imageService.DeleteImage(delivery.LicenteImagePath);
        }

        var fileName = await _imageService.SaveImageAsync(request.ImageBase64);

        delivery.UpdateImagePath(fileName);
        await _deliveryRepository.UpdateDeliveryAsync(delivery);

        return Unit.Value;
    }
}
