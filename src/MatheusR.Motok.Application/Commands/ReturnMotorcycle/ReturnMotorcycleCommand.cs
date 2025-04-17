using MediatR;

namespace MatheusR.Motok.Application.Commands.ReturnMotorcycle;
public class ReturnMotorcycleCommand : IRequest<Unit>
{
    public ReturnMotorcycleCommand(Guid id, DateTime returnDate)
    {
        Id = id;
        ReturnDate = returnDate;
    }

    public Guid Id { get; set; }
    public DateTime ReturnDate { get; set; }
}
