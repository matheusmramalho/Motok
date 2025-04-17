using MatheusR.Motok.Domain.Enums;
using MediatR;

namespace MatheusR.Motok.Application.Commands.CreateRent;
public class CreateRentCommand : IRequest<Unit>
{
    public Guid EntregadorId { get; set; }
    public Guid MotoId { get; set; }
    public DateTime DataPrevisaoTermino { get; set; }
    public RentPlanType Plano { get; set; }
}
