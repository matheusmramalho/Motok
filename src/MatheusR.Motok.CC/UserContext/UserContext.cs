using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MatheusR.Motok.CC.UserContext;
public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId => GetUserId();
    public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;
    public string? UserRole => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    private Guid? GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }
        return null;
    }
}