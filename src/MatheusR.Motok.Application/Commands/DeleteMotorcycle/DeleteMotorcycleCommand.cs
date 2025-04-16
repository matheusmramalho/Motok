using MediatR;

namespace MatheusR.Motok.Application.Commands.DeleteMotorcycle;
public class DeleteMotorcycleCommand : IRequest
{
    public DeleteMotorcycleCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
