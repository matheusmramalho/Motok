using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.Application.Commands.CreateDelivery;
using MatheusR.Motok.Application.Commands.UpdateDeliveryLicenceImage;
using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MatheusR.Motok.API.Controllers;

[Route("entregadores")]
[ApiController]
//[ApiExplorerSettings(GroupName = "entregadores")]
public class DeliveryController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeliveryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MotorcycleOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryCommand command, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(command, cancellationToken);
        return Created();
    }

    [HttpPost("{id:guid}/cnh")]
    [ProducesResponseType(typeof(MotorcycleOutput), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadDeliveryLicenceNumberImage([FromRoute] Guid id, [FromBody] UpdateDeliveryLicenceImageModel input, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(input.ImagemCnh))
            return BadRequest("Dados inválidos.");

        var output = await _mediator.Send(new UpdateDeliveryLicenceImageCommand(id, input.ImagemCnh), cancellationToken);
        return Created();
    }
}
