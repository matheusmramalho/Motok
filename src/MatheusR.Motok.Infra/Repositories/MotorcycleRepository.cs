using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.OtherTables;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.Infra.Repositories;
public class MotorcycleRepository : IMotorcycleRepository
{
    private readonly AppDbContext _context;

    public MotorcycleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateMotorcycle(Motorcycle motorcycle)
    {
        await _context.Motorcycles.AddAsync(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMotorcycle(Motorcycle motorcycle)
    {
        _context.Motorcycles.Remove(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task<Motorcycle?> GetMotorcycleById(Guid id)
    {
        var motorcycle = await _context.Motorcycles.FindAsync(id);
        return motorcycle;
    }

    public async Task<Motorcycle?> GetMotorcycleByLicencePlate(string? licencePlate)
    {
        var motorcycle = await _context.Motorcycles.AsNoTracking().FirstOrDefaultAsync(m => m.LicencePlate.Equals(licencePlate));
        return motorcycle;
    }

    public async Task<List<Motorcycle>> GetAllMotorcycles()
    {
        var motorcycles = await _context.Motorcycles.AsNoTracking().ToListAsync();
        return motorcycles;
    }

    public async Task UpdateMotorcycle(Motorcycle motorcycle)
    {
        _context.Motorcycles.Update(motorcycle);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> LicencePlateExists(string licencePlate)
    {
        return await _context.Motorcycles.AnyAsync(m => m.LicencePlate == licencePlate);
    }

    public async Task SaveMotorcycle2024(Motorcycle2024 motorcycle)
    {
        await _context.Motorcycles2024.AddAsync(motorcycle);
        await _context.SaveChangesAsync();
    }
}
