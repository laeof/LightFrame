using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository noteRepository;
    public NoteService(INoteRepository noteRepository) {
        this.noteRepository = noteRepository;
    }
    public async Task<IResult<Note>> AddNoteAsync(Note note)
    {
        var result = await noteRepository.AddNoteAsync(note);

        return result;
    }

    public async Task<IResult<Note>> GetNoteIdAsync(Guid id)
    {
        var result = await noteRepository.GetNoteByIdAsync(id);

        return result;
    }

    public async Task<IResult<List<Note>>> GetNotesDayAsync(string day)
    {
        var result = await noteRepository.GetNotesForADayAsync(day);

        return result;
    }

    public async Task<IResult<Note>> ModifyNoteAsync(Note note)
    {
        var result = await noteRepository.UpdateNoteAsync(note);

        return result;
    }
}