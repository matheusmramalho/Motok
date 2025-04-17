using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.CC.Repositories;
public interface IRoleRepository
{
    Task AddRole(Role role);
    Task<Role?> GetRoleByName(string roleName);
}
