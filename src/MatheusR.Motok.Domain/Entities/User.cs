using MatheusR.Motok.Domain.Entities.Base;

namespace MatheusR.Motok.Domain.Entities;
public class User : Entity
{
    public User(string name, string userName, string password, Role role)
    {
        Name = name;
        UserName = userName;
        Password = password;
        Role = role;
        RoleId = role.Id;
    }

    protected User() { }

    public string Name { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public Guid RoleId { get; private set; }

    public void SetPassword(string password)
    {
        Password = password;
    }
}
