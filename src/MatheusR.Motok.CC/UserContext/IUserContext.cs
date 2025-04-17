using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.CC.UserContext;
public interface IUserContext
{
    Guid? UserId { get; }
    string? UserName { get; }
    string? UserRole { get; }
    bool IsAuthenticated { get; }
}
