using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserRoleRepository userRoleRepository;
    private readonly IRefreshTokensRepository refreshTokensRepository;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UserService(IRefreshTokensRepository refreshTokensRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.refreshTokensRepository = refreshTokensRepository;
        this.userRepository = userRepository;
        this.userRoleRepository = userRoleRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<IResult<bool>> AddRefreshTokenAsync(Guid userId, string newRefreshToken)
    {
        var tokenSavedResult = await refreshTokensRepository.CreateRefreshTokenAsync(userId, newRefreshToken);

        if (tokenSavedResult.IsFailure) return Result<bool>.Faillure(tokenSavedResult.Error);

        return Result<bool>.Success(true);
    }

    public async Task<IResult<bool>> ExistsByEmailAsync(string email)
    {
        var userResult = await userRepository.GetUserByEmailAsync(email);

        if (userResult.IsFailure) return Result<bool>.Faillure(userResult.Error);

        return Result<bool>.Success(true);
    }

    public async Task<IResult<User>> GetUserByIdAsync(Guid id)
    {
        var userResult = await userRepository.GetUserByIdAsync(id);

        return userResult;
    }

    public async Task<IResult<User>> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var userIdWithRefreshTokenResult = await refreshTokensRepository.GetUseridWithTokenAsync(refreshToken);

        if (userIdWithRefreshTokenResult.IsFailure) return Result<User>.Faillure(userIdWithRefreshTokenResult.Error);

        var userResult = await userRepository.GetUserByIdAsync(userIdWithRefreshTokenResult.Value);

        return userResult;
    }

    public async Task<IResult<User>> RegisterUserAsync(Register dto, string passwordHash)
    {
        var userResult = await userRepository.CreateUserAsync(new()
        {
            Email = dto.Email,
            PasswordHash = passwordHash,
            Phone = dto.Phone,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName
        });

        if (userResult.IsFailure) return Result<User>.Faillure(userResult.Error);

        //fixme!!! guid.empty change to default value for user role
        var roleAddedResult = await userRoleRepository.AddRoleToUser(userResult.Value.Id, Guid.Empty);

        if (roleAddedResult.IsFailure) return Result<User>.Faillure(roleAddedResult.Error);

        return Result<User>.Success(userResult.Value);
    }

    public Task<IResult<bool>> RevokeRefreshTokenAsync(string oldRefreshToken)
    {
        var revokedResult = refreshTokensRepository.RevokeRefreshTokenAsync(oldRefreshToken);

        return revokedResult;
    }

    public async Task<IResult<User>> GetUser()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user == null)
            return Result<User>.Faillure(new("401", "No user"));

        var userid = user.FindFirst(ClaimTypes.Name);

        if (userid == null)
            return Result<User>.Faillure(new("401", "No user id"));

        return await userRepository.GetUserByIdAsync(Guid.Parse(userid.Value));
    }
}