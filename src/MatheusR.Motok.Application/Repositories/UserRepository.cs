using MatheusR.Motok.CC.Repositories;
using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.Application.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUser(string username)
    {
        return await _context.Users.Include(u=> u.Role).FirstOrDefaultAsync(u => u.UserName == username);
    }
}
