namespace AuthService.Application.Contracts;

using AuthService.Domain.Enums;
public sealed record AuthResponse (
    Guid UserId,
    string Email,
    UserRole Role
);