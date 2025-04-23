using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UserRoleRepository : IUserRoleRepository
{
    private AppDbContext context;
    public UserRoleRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<IResult<bool>> AddRoleToUser(Guid userId, Guid roleId)
    {
        var userRole = new UserRole
        {
            UserId = userId,
            RoleId = roleId,
        };
        try
        {
            context.Entry(userRole).State = EntityState.Added;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<bool>.Faillure(new("500", ex.Message));
        }

        return Result<bool>.Success(true);
    }
}