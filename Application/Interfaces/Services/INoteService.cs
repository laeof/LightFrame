using Domain.Entities;

namespace Application.Interfaces.Services;

public interface INoteService {
    Task<IResult<Note>> GetNoteIdAsync(Guid id);
    Task<IResult<List<Note>>> GetNotesDayAsync(string day);
    Task<IResult<Note>> AddNoteAsync(Note note);
    Task<IResult<Note>> ModifyNoteAsync(Note note);
    Task<IResult<List<Note>>> GetMyNotes(Guid id);
}