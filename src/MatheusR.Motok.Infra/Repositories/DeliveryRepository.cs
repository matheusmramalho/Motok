using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.Repositories;
using MatheusR.Motok.Infra.Database;

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
}
