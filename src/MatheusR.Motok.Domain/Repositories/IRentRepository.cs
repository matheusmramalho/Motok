using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Domain.Repositories;
public interface IRentRepository
{
    Task CreateRent(Rent rent);
    Task<Rent?> GetRentById(Guid id);
    Task UpdateRent(Rent rent);
}
