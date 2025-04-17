using MatheusR.Motok.CC.Repositories;
using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.Application.Repositories;
public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRole(Role role)
    {
        await _context.Roles.AddAsync(role);
        await _context.SaveChangesAsync();
    }

    public async Task<Role?> GetRoleByName(string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        return role;
    }
}
