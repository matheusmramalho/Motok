using MatheusR.Motok.Application.Commands.Common;
using MatheusR.Motok.Application.Commands.CreateRent;
using MatheusR.Motok.Application.Commands.ReturnMotorcycle;
using MatheusR.Motok.Application.Queries.GetRentById;
using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatheusR.Motok.API.Controllers;

[Route("locacao")]
[ApiController]
[Authorize(Roles = "DELIVERY")]
public class RentController : ControllerBase
{
    private readonly IMediator _mediator;

    public RentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RentMotorcycle([FromBody] CreateRentCommand command, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(command, cancellationToken);
        return Created();
    }

    [HttpPut("{id:guid}/devolucao")]
    [ProducesResponseType(typeof(ApiResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReturnMotorcycle([FromRoute] Guid id, [FromBody] UpdateRentReturnDateInputModel inputModel, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ReturnMotorcycleCommand(id, inputModel.ReturnDate), cancellationToken);
        return Ok(new ApiResponseModel("Data de devolução informada com sucesso"));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(List<MotorcycleOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponseError), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRentById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var output = await _mediator.Send(new GetRentByIdQuery(id), cancellationToken);
        return Ok(output);
    }
}
