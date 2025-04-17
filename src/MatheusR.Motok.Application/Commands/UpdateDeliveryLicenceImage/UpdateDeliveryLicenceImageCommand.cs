using MediatR;

namespace MatheusR.Motok.Application.Commands.UpdateDeliveryLicenceImage;
public class UpdateDeliveryLicenceImageCommand: IRequest<Unit>
{
    public UpdateDeliveryLicenceImageCommand(Guid id, string imageBase64)
    {
        Id = id;
        ImageBase64 = imageBase64;
    }
    public Guid Id { get; set; }
    public string ImageBase64 { get; set; }
}
