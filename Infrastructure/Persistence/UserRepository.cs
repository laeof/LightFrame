using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext context;
    public UserRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IResult<User>> CreateUserAsync(User user)
    {
        try
        {
            user.PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/User-avatar.svg/2048px-User-avatar.svg.png";
            context.Entry(user).State = EntityState.Added;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<User>.Faillure(new("500", ex.Message));
        }

        return Result<User>.Success(user);
    }

    public Task<IResult<bool>> DisableUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult<User>> GetUserByEmailAsync(string email)
    {
        var userResult = await context.Users.FirstOrDefaultAsync(e => e.Email == email);

        if (userResult == null) return Result<User>.Faillure(new("404", "User not found"));

        return Result<User>.Success(userResult);
    }

    public async Task<IResult<User>> GetUserByIdAsync(Guid id)
    {
        var userResult = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (userResult == null) return Result<User>.Faillure(new("404", "User not found"));

        return Result<User>.Success(userResult);
    }

    public Task<IResult<User>> ModifyUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}