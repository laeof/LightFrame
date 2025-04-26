using Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AddNoteUseCase>();
        services.AddScoped<AuthenticateUserUseCase>();
        services.AddScoped<FireUseCase>();
        services.AddScoped<HireUseCase>();
        services.AddScoped<ModifyNoteUseCase>();
        services.AddScoped<RefreshTokenUseCase>();
        services.AddScoped<RegisterUserUseCase>();

        return services;
    }
}