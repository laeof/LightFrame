using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IRoomService, RoomService>();

        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IPhotographerRepository, PhotographerRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();

        // services.AddDbContext<AppDbContext>(options =>
        // {
        //     options.UseNpgsql(
        //         $"Host={Environment.GetEnvironmentVariable("ASPNETCORE_AAUTHENTIC_DB_SERVER")};" +
        //         $"Port={Environment.GetEnvironmentVariable("ASPNETCORE_AAUTHENTIC_DB_PORT")};" +
        //         $"Username={Environment.GetEnvironmentVariable("ASPNETCORE_AAUTHENTIC_DB_USER")};" +
        //         $"Password={Environment.GetEnvironmentVariable("ASPNETCORE_AAUTHENTIC_DB_PASS")};" +
        //         $"Database={Environment.GetEnvironmentVariable("ASPNETCORE_AAUTHENTIC_DB_NAME")};");
        // });

        services.AddDbContext<AppDbContext>(x => x.UseNpgsql("Host=127.0.0.1;Port=5432;Username=postgres;Password=bt7iC4nN07T0f1nDmyp4ss;Database=lightframe"));

        return services;
    }
}