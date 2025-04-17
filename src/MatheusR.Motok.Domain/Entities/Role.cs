using MatheusR.Motok.Domain.Entities.Base;

namespace MatheusR.Motok.Domain.Entities;
public class Role : Entity
{
    public Role(string name)
    {
        Name = name;
    }

    protected Role() { }

    public string Name { get; set; }
}
