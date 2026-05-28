namespace AuthService.Application.UseCases.Auth.Login;

using AuthService.Application.Abstractions;
using AuthService.Application.Contracts;

public interface ILoginUseCase : IUseCase<LoginRequest, AuthResponse>
{
}

