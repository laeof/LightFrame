using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;

public class AddNoteUseCase
{
    public readonly INoteService noteService;

    public AddNoteUseCase(INoteService noteService)
    {
        this.noteService = noteService;
    }

    public async Task<IResult<Note>> ExecuteAsync(Slot slot, string customerPhone, string customerName, Guid RoomId, Guid customerId = default)
    {
        var result = await noteService.AddNoteAsync(new() {
            Day = slot.Day,
            CustomerId = customerId,
            CustomerName = customerName,
            Start = slot.Start,
            End = slot.End,
            CustomerPhone = customerPhone,
            RoomId = RoomId
        });

        return result;
    }
}