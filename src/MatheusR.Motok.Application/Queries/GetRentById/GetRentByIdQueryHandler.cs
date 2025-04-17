using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.Application.Queries.OutputQueryModels;
using MatheusR.Motok.CC.UserContext;
using MatheusR.Motok.Domain.Repositories;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetRentById;
public class GetRentByIdQueryHandler : IRequestHandler<GetRentByIdQuery, RentByIdOutputModel>
{
    private readonly IRentRepository _rentRepository;
    private readonly IUserContext _userContext;

    public GetRentByIdQueryHandler(IRentRepository rentRepository, IUserContext userContext)
    {
        _rentRepository = rentRepository;
        _userContext = userContext;
    }

    public async Task<RentByIdOutputModel> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
    {
        // TODO: Não permitir acesso entre usuários, precisa vincular o usuário com delivery e ajustar o userContext.

        var rent = await _rentRepository.GetRentById(request.Id);
        if (rent == null)
            throw new MotokNotFoundException($"Rent not found.");

        var (valorTotal, valorDiaria) = rent.CalculateDailyPrice();

        var rentOutput = RentByIdOutputModel.FromEntity(rent, valorDiaria, valorTotal);

        return rentOutput;
    }
}
