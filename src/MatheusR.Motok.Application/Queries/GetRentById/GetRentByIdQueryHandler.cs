using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Application.Queries.OutputQueryModels;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetRentById;
public class GetRentByIdQueryHandler : IRequestHandler<GetRentByIdQuery, RentByIdOutputModel>
{
    private readonly IRentRepository _rentRepository;

    public GetRentByIdQueryHandler(IRentRepository rentRepository)
    {
        _rentRepository = rentRepository;
    }

    public async Task<RentByIdOutputModel> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        var rent = await _rentRepository.GetRentById(request.Id);
        if (rent == null)
            throw new MotokNotFoundException($"Rent not found.");

        var (valorTotal, valorDiaria) = rent.CalculateDailyPrice();

        var rentOutput = RentByIdOutputModel.FromEntity(rent, valorDiaria, valorTotal);

        return rentOutput;
    }
}
