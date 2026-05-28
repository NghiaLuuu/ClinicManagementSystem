namespace AuthService.Application.UseCases.Auth.RegisterDentist;

using AuthService.Application.Abstractions;
using AuthService.Application.Contracts;

public interface IRegisterDentistUseCase : IUseCase<RegisterRequest, AuthResponse>
{
}
