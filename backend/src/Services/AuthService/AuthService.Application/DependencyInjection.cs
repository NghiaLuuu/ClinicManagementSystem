namespace AuthService.Application;

using AuthService.Application.UseCases.Auth.Login;
using AuthService.Application.UseCases.Auth.RegisterDentist;
using AuthService.Application.UseCases.Auth.RegisterPatient;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterPatientUseCase>();
        services.AddScoped<RegisterDentistUseCase>();
        services.AddScoped<LoginUseCase>();

        return services;
    }
}