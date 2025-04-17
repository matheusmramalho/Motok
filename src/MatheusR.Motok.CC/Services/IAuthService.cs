using MatheusR.Motok.CC.InputModels;

namespace MatheusR.Motok.CC.Services;
public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task RegisterAsync(RegisterUserInputModel registerUserInputModel);
}
