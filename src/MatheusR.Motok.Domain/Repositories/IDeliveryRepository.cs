using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Domain.Repositories;
public interface IDeliveryRepository
{
    Task CreateDeliveryAsync(Delivery delivery);
    Task<bool> ExistsByCnh(string cnh);
    Task<bool> ExistsByCnpj(string cnpj);
    Task<Delivery?> GetDeliveryById(Guid id);
    Task UpdateDeliveryAsync(Delivery delivery);
}
