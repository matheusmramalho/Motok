using MatheusR.Motok.Application.Commands.CreateDelivery;
using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Application.Extensions;
public static class DeliveryExtensions
{
    public static Delivery ToEntity(this CreateDeliveryCommand command)
    {
        if (command == null)
            return null;

        return new Delivery(command.Nome, command.Cnpj, DateOnly.FromDateTime(command.DataNascimento), command.NumeroCnh, command.TipoCnh);
    }
}
