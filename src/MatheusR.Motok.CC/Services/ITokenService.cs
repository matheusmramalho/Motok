using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.CC.Services;
public interface ITokenService
{
    string GetToken(string token);
    string GenerateToken(User user);
}
