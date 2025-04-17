using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Repositories;
using MatheusR.Motok.CC.Services;
using MatheusR.Motok.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MatheusR.Motok.Application.Services;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ITokenService tokenService,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await _userRepository.GetUser(username);

        if (user == null)
        {
            throw new MotokUnauthorizedAccessException();
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);

        if (verificationResult != PasswordVerificationResult.Success)
        {
            throw new MotokUnauthorizedAccessException();
        }

        return _tokenService.GenerateToken(user);
    }

    public async Task RegisterAsync(RegisterUserInputModel inputModel)
    {
        var existingUser = await _userRepository.GetUser(inputModel.Username);
        if (existingUser != null)
        {
            throw new MotokApplicationException("Usuário já está em uso");
        }

        var role = await _roleRepository.GetRoleByName(inputModel.Rolename.Trim().ToUpper()) ?? throw new MotokNotFoundException("Role não encontrada");

        var user = new User(inputModel.Name, inputModel.Username, string.Empty, role);
        var password = _passwordHasher.HashPassword(user, inputModel.Password);
        user.SetPassword(password);

        await _userRepository.AddUser(user);
    }
}
