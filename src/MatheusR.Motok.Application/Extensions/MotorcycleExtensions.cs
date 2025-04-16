using MatheusR.Motok.Application.Commands.CreateMotorcycle;
using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Application.Extensions;
public static class MotorcycleExtensions
{
    public static Motorcycle ToEntity(this CreateMotorcycleCommand command)
    {
        if (command == null)
            return null;

        return new Motorcycle(command.Identifier, command.Year, command.Model, command.LicencePlate);
    }
}
