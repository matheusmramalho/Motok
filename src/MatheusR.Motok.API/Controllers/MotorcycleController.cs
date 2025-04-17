using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.Application.Commands.DeleteMotorcycle;
using MatheusR.Motok.Application.Commands.UpdateMotorcycleLicencePlate;
using MatheusR.Motok.Application.Queries.GetMotorcycles;
using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatheusR.Motok.API.Controllers;

[Route("motos")]
[ApiController]
[Authorize(Roles = "ADMIN")]
//[ApiExplorerSettings(GroupName = "motos")]
public class MotorcycleController : ControllerBase
{
    private readonly IMediator _mediator;

    public MotorcycleController(
        ILogger<MotorcycleController> logger,
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MotorcycleOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateMotorcycle([FromBody] CreateMotorcycleCommand command, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(CreateMotorcycle), new { output.Id }, output);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MotorcycleOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMotorcycles([FromQuery] string? licencePlate, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetMotorcyclesCommand(licencePlate), cancellationToken);
        return Ok(output);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(List<MotorcycleOutput>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMotorcycleById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetMotorcycleByIdCommand(id), cancellationToken);
        return Ok(output);
    }

    [HttpPatch("{id:guid}/placa")]
    [ProducesResponseType(typeof(List<MotorcycleOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateLicencePlace([FromRoute] Guid id, [FromBody] UpdateMotorcycleLicencePlateInputModel inputModel, CancellationToken cancellationToken)
    {
        await _mediator.Send(new UpdateLicencePlateCommand(id, inputModel.Placa), cancellationToken);
        return Ok(new ApiResponseModel("Placa modificada com sucesso"));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(List<MotorcycleOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteMotorcycle([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMotorcycleCommand(id), cancellationToken);
        return Ok();
    }
}
