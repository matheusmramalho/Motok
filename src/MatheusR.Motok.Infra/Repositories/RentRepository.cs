using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Infra.Database;

namespace MatheusR.Motok.Infra.Repositories;
public class RentRepository : IRentRepository
{
    private readonly AppDbContext _context;

    public RentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateRent(Rent rent)
    {
        await _context.Rents.AddAsync(rent);
        await _context.SaveChangesAsync();
    }

    public async Task<Rent?> GetRentById(Guid id)
    {
        return await _context.Rents.FindAsync(id);
    }

    public async Task UpdateRent(Rent rent)
    {
        _context.Rents.Update(rent);
        await _context.SaveChangesAsync();
    }
}
