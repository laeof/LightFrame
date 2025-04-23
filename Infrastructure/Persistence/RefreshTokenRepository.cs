using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class RefreshTokensRepository : IRefreshTokensRepository
{
    private AppDbContext context;
    public RefreshTokensRepository(
        AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IResult<UserRefreshToken>> CreateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var userRefreshToken = new UserRefreshToken
        {
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(1),
            isRevoked = false,
            UserId = userId,
        };

        try
        {
            context.Entry(userRefreshToken).State = EntityState.Added;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<UserRefreshToken>.Faillure(new("500", ex.Message));
        }

        return Result<UserRefreshToken>.Success(userRefreshToken);
    }

    public async Task<IResult<UserRefreshToken>> GetRefreshTokenByUserIdAsync(Guid id)
    {
        var refreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(r => r.UserId == id);

        if (refreshToken == null) return Result<UserRefreshToken>.Faillure(new("404", "Token not found"));

        return Result<UserRefreshToken>.Success(refreshToken);
    }

    public async Task<IResult<Guid>> GetUseridWithTokenAsync(string token)
    {
        var refreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(r => r.Token == token);

        if (refreshToken == null) return Result<Guid>.Faillure(new("404", "Token not found"));

        return Result<Guid>.Success(refreshToken.UserId);
    }

    public async Task<IResult<bool>> RevokeRefreshTokenAsync(string token)
    {
        var refreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(r => r.Token == token);

        if (refreshToken == null) return Result<bool>.Faillure(new("404", "Token not found"));

        try
        {
            refreshToken.isRevoked = true;
            context.Entry(refreshToken).State = EntityState.Modified;

        }
        catch (Exception ex)
        {
            return Result<bool>.Faillure(new("500", ex.Message));
        }

        return Result<bool>.Success(true);
    }
}