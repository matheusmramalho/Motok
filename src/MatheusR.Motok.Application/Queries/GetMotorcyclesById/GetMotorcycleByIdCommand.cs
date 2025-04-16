using MatheusR.Motok.Application.Commands.Common;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetMotorcycles;
public class GetMotorcycleByIdCommand : IRequest<MotorcycleOutput>
{
    public Guid Id { get; private set; }
    public GetMotorcycleByIdCommand(Guid id) => Id = id;
}
