using BackendNet.Application.Auth.Login;
using BackendNet.Application.Auth.Register;
using BackendNet.Infrastructure.Repositories;
using BackendNet.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace BackendNet.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        services.AddScoped<ITokenProvider, JwtTokenProvider>();

        return services;
    }
}