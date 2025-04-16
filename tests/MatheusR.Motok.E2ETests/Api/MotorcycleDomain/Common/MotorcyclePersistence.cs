using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.E2ETests.Base;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.E2ETests.Api.MotorcycleDomain.Common;
public class MotorcyclePersistence : BaseFixture
{
    private readonly AppDbContext _dbContext;

    public MotorcyclePersistence(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ClearAllAsync()
    {
        var allMotorcycles = await _dbContext.Motorcycles.ToListAsync();
        _dbContext.Motorcycles.RemoveRange(allMotorcycles);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Motorcycle?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Motorcycles.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Motorcycle>> GetAllAsync()
    {
        return await _dbContext.Motorcycles.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Motorcycle motorcycle)
    {
        await _dbContext.Motorcycles.AddAsync(motorcycle);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddListAsync(List<Motorcycle> motorcycles)
    {
        await _dbContext.Motorcycles.AddRangeAsync(motorcycles);
        await _dbContext.SaveChangesAsync();
    }
}
