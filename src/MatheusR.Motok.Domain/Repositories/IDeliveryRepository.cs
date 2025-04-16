using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Domain.Repositories;
public interface IDeliveryRepository
{
    Task CreateDeliveryAsync(Delivery delivery);
    Task UpdateDeliveryAsync(Delivery delivery);
}
