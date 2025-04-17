using MatheusR.Motok.Application.Queries.OutputQueryModels;
using MediatR;

namespace MatheusR.Motok.Application.Queries.GetRentById;
public class GetRentByIdQuery : IRequest<RentByIdOutputModel>
{
    public GetRentByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
