using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace MatheusR.Motok.Infra.Repositories;
public class DeliveryRepository : IDeliveryRepository
{
    private readonly AppDbContext _context;

    public DeliveryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateDeliveryAsync(Delivery delivery)
    {
        await _context.Deliveries.AddAsync(delivery);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDeliveryAsync(Delivery delivery)
    {
        _context.Deliveries.Update(delivery);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByCnpj(string cnpj)
    {
        return await _context.Deliveries.AsNoTracking().AnyAsync(m => m.Cnpj == cnpj);
    }

    public async Task<bool> ExistsByCnh(string cnh)
    {
        return await _context.Deliveries.AsNoTracking().AnyAsync(m => m.LicenceNumber == cnh);
    }

    public async Task<Delivery?> GetDeliveryById(Guid id)
    {
        var delivery = await _context.Deliveries.FindAsync(id);
        return delivery;
    }
}
