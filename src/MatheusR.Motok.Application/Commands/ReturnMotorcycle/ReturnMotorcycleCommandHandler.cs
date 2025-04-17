using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Commands.ReturnMotorcycle;
public class ReturnMotorcycleCommandHandler : IRequestHandler<ReturnMotorcycleCommand, Unit>
{
    private readonly IRentRepository _rentRepository;

    public ReturnMotorcycleCommandHandler(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    public async Task<Unit> Handle(ReturnMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var rent = await _rentRepository.GetRentById(request.Id);

        if (rent is null)
            throw new MotokApplicationException("Aluguel não encontrado");

        rent.UpdateFinalDate(request.ReturnDate);

        await _rentRepository.UpdateRent(rent);

        return Unit.Value;
    }
}
