using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.Entities.Base;

namespace MatheusR.Motok.Domain.OtherTables;
public class Motorcycle2024: Entity
{
    public Motorcycle Motorcycle { get; set; }
    public Guid MotorcycleId { get; set; }
 
    protected Motorcycle2024() { }

    public Motorcycle2024(Motorcycle motorcycle)
    {
        Motorcycle = motorcycle;
        MotorcycleId = motorcycle.Id;
    }
}
