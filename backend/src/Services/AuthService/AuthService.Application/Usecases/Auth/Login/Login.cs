namespace AuthService.Application.UseCases.Auth.Login;

using AuthService.Application.Abstractions;
using AuthService.Application.Contracts;
using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Repositories;
using AuthService.Domain.ValueObjects;

public sealed class LoginUseCase : ILoginUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> ExecuteAsync(LoginRequest request, CancellationToken ct = default)
    {
        var email = Email.Create(request.Email);

        var user = await _userRepository.GetByEmailAsync(email);
        if (user is null || user.IsActive == false)
            throw new UnauthorizedException("Invalid credentials.");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash.Value))
            throw new UnauthorizedException("Invalid credentials.");

        return new AuthResponse(user.Id, user.Email.Value, user.Role);
    }
}