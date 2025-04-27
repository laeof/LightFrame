using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class NoteRepository : INoteRepository
{
    private AppDbContext context;
    public NoteRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task<IResult<Note>> AddNoteAsync(Note note)
    {
        try
        {
            context.Entry(note).State = EntityState.Added;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<Note>.Faillure(new("500", ex.Message));
        }

        return Result<Note>.Success(note);
    }

    public async Task<IResult<Note>> GetNoteByIdAsync(Guid id)
    {
        var noteResult = await context.Notes.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        if (noteResult == null) return Result<Note>.Faillure(new("404", "Note not found"));

        return Result<Note>.Success(noteResult);
    }

    public async Task<IResult<List<Note>>> GetNotesForADayAsync(string day)
    {
        var noteResult = await context.Notes.Where(u => u.Day == day && !u.IsDisabled).ToListAsync();

        if (noteResult == null) return Result<List<Note>>.Faillure(new("404", "Note not found"));

        return Result<List<Note>>.Success(noteResult);
    }

    public async Task<IResult<List<Note>>> GetNotesWithUserId(Guid userId)
    {
        var noteResult = await context.Notes.Where(x => x.CustomerId == userId).ToListAsync();

        if (noteResult == null) return Result<List<Note>>.Faillure(new("404", "Note not found"));

        return Result<List<Note>>.Success(noteResult);
    }

    public async Task<IResult<Note>> UpdateNoteAsync(Note note)
    {
        try
        {
            context.Entry(note).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result<Note>.Faillure(new("500", ex.Message));
        }

        return Result<Note>.Success(note);
    }
}