namespace AuthService.Application.UseCases.Auth.RegisterPatient;

using AuthService.Application.Abstractions;
using AuthService.Application.Contracts;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Repositories;
using AuthService.Domain.ValueObjects;

public sealed class RegisterPatientUseCase : IUseCase<RegisterRequest, AuthResponse>
{
    private static readonly Guid SystemUserId = Guid.Empty;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterPatientUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponse> ExecuteAsync(RegisterRequest request, CancellationToken ct = default)
    {
        var username = Username.Create(request.Username);
        var email = Email.Create(request.Email);

        var existing = await _userRepository.GetByEmailAsync(email.Value);
        if (existing is not null)
            throw new ConflictException("Email already exists.");

        var hash = _passwordHasher.Hash(request.Password);
        var passwordHash = PasswordHash.Create(hash);

        var user = User.CreatePatient(username, email, passwordHash, SystemUserId);
        await _userRepository.AddAsync(user);

        return new AuthResponse(user.Id, user.Email.Value, user.Role);
    }
}