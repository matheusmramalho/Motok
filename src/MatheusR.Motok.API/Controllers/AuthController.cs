using MatheusR.Motok.CC.InputModels;
using MatheusR.Motok.CC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatheusR.Motok.Application.Controllers;

[ApiController]
[Route("auth")]
[Authorize(Roles = "ADMIN")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register([FromBody] RegisterUserInputModel model)
    {
        _logger.LogInformation("Tentativa de registro com usuário: {Username}", model.Username);

        await _authService.RegisterAsync(model);

        _logger.LogInformation("Usuário registrado com sucesso: {Username}", model.Username);
        return Created();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginInputModel model)
    {
        _logger.LogInformation("Tentativa de login com usuário: {Username}", model.Username);

        var token = await _authService.LoginAsync(model.Username, model.Password);

        _logger.LogInformation("Login bem-sucedido para: {Username}", model.Username);
        return Ok(new { token });
    }
}
