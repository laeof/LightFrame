using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class PhotographerRepository : IPhotographerRepository
{
    private readonly AppDbContext context;
    public PhotographerRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IResult<Photographer>> FirePhotographer(Guid id)
    {
        var photographerResult = await context.Photographers.FirstOrDefaultAsync(u => u.Id == id);

        if (photographerResult == null) return Result<Photographer>.Faillure(new("404", "Note not found"));

        photographerResult.IsDisabled = true;

        try
        {
            context.Entry(photographerResult).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<Photographer>.Faillure(new("500", ex.Message));
        }

        return Result<Photographer>.Success(photographerResult);
    }

    public async Task<IResult<Photographer>> HirePhotographer(Photographer photographer)
    {
        try
        {
            context.Entry(photographer).State = EntityState.Added;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<Photographer>.Faillure(new("500", ex.Message));
        }

        return Result<Photographer>.Success(photographer);
    }
}