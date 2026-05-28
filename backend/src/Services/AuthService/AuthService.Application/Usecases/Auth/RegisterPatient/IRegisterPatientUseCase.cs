namespace AuthService.Application.UseCases.Auth.RegisterPatient;

using AuthService.Application.Abstractions;
using AuthService.Application.Contracts;

public interface IRegisterPatientUseCase : IUseCase<RegisterRequest, AuthResponse>
{
}
