namespace AuthService.Infrastructure;

using AuthService.Domain.Interfaces;
using AuthService.Domain.Repositories;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AuthDb")
            ?? throw new InvalidOperationException("Missing ConnectionStrings:AuthDb");

        services.AddDbContext<AuthDbContext>(opt => opt.UseNpgsql(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        return services;
    }
}