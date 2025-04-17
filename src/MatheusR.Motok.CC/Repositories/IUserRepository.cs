using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.CC.Repositories;
public interface IUserRepository
{
    Task AddUser(User user);
    Task<User?> GetUser(string username);
}
