using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using BackendNet.Application.Auth.Login;
using BackendNet.Application.Auth.Register;
using Microsoft.Extensions.DependencyInjection;
using BackendNet.Application.Users;
namespace BackendNet.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<RegisterUserHandler>();

        services.AddScoped<LoginUserHandler>();

        services.AddScoped<UserService>();

        services.AddValidatorsFromAssemblyContaining<RegisterUserValidator>();

        return services;
    }
}