using MatheusR.Motok.Domain.Entities;
using MatheusR.Motok.Domain.OtherTables;

namespace MatheusR.Motok.Domain.Repositories;
public interface IMotorcycleRepository
{
    Task CreateMotorcycle(Motorcycle motorcycle);
    Task<Motorcycle?> GetMotorcycleById(Guid Id);
    Task<Motorcycle?> GetMotorcycleByLicencePlate(string? licencePlate);
    Task UpdateMotorcycle(Motorcycle motorcycle);
    Task DeleteMotorcycle(Motorcycle motorcycle);
    Task<List<Motorcycle>> GetAllMotorcycles();
    Task<bool> LicencePlateExists(string licencePlate);
    Task SaveMotorcycle2024(Motorcycle2024 motorcycle);
}
