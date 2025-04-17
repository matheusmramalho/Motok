using MatheusR.Motok.Application.Commands.CreateRent;
using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Application.Extensions;
public static class RentExtensions
{
    public static Rent ToEntity(this CreateRentCommand command, Delivery delivery, Motorcycle motorcycle)
    {
        return new Rent(DateTime.UtcNow.AddDays(1), command.DataPrevisaoTermino, 0m, command.Plano, delivery, motorcycle, null);
    }
}
