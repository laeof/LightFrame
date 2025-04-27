using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;

public class ModifyNoteUseCase
{
    public readonly INoteService noteService;

    public ModifyNoteUseCase(INoteService noteService)
    {
        this.noteService = noteService;
    }

    public async Task<IResult<Note>> ExecuteAsync(NoteDto dto)
    { 
        Note note = new() {
            CustomerId = dto.CustomerId,
            CustomerName = dto.CustomerName,
            CustomerPhone = dto.CustomerPhone,
            Day = dto.Day,
            End = dto.End,
            Start = dto.Start,
            Id = dto.Id,
            IsDisabled = dto.IsDisabled,
            RoomId = dto.RoomId,
            PaidState = dto.PaidState,
        };

        var result = await noteService.ModifyNoteAsync(note);

        return result;
    }
}