using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface INoteRepository {
    Task<IResult<Note>> GetNoteByIdAsync(Guid id);
    Task<IResult<List<Note>>> GetNotesForADayAsync(string day);
    Task<IResult<Note>> AddNoteAsync(Note note);
    Task<IResult<Note>> UpdateNoteAsync(Note note);
}