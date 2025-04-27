using API.Requests;
using Application.Common;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class NoteController : ControllerBase
{
    private readonly INoteService noteService;
    private readonly AddNoteUseCase addNoteUseCase;
    private readonly ModifyNoteUseCase modifyNoteUseCase;
    public NoteController(INoteService noteService, AddNoteUseCase addNoteUseCase, ModifyNoteUseCase modifyNoteUseCase)
    {
        this.noteService = noteService;
        this.addNoteUseCase = addNoteUseCase;
        this.modifyNoteUseCase = modifyNoteUseCase;
    }

    [HttpGet("GetId/{id}")]
    public async Task<IActionResult> GetNote(Guid id)
    {
        var result = await noteService.GetNoteIdAsync(id);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("GetNotesWithUserId/{userId}")]
    public async Task<IActionResult> GetMyNotes(Guid userId)
    {
        var result = await noteService.GetMyNotes(userId);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("GetDay")]
    public async Task<IActionResult> GetAllNotesForADay([FromHeader]DayRequest request)
    {
        var result = await noteService.GetNotesDayAsync(request.Day + request.Month + request.Year);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("AddNoteGuest")]
    public async Task<IActionResult> AddNoteGuest(AddNoteAsGuestRequest request)
    {
        var result = await addNoteUseCase.ExecuteAsync(new(request.Start, request.End, request.Day), request.PhoneNumber, request.Name, request.RoomId);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("AddNoteAuth")]
    public async Task<IActionResult> AddNote(AddNoteAsAuthorizedRequest request)
    {
        var result = await addNoteUseCase.ExecuteAsync(new(request.Start, request.End, request.Day), request.PhoneNumber, request.Name, request.RoomId, request.Id);

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPut("Pay")]
    public async Task<IActionResult> PayNote([FromQuery]Guid id)
    {
        var checkResult = await noteService.GetNoteIdAsync(id);

        if (checkResult.IsFailure) return NotFound(checkResult.Error);

        var modifyResult = await modifyNoteUseCase.ExecuteAsync(new()
        {
            Id = checkResult.Value.Id,
            CustomerId = checkResult.Value.CustomerId,
            CustomerName = checkResult.Value.CustomerName,
            CustomerPhone = checkResult.Value.CustomerPhone,
            Day = checkResult.Value.Day,
            Start = checkResult.Value.Start,
            End = checkResult.Value.End,
            PaidState = true,
            RoomId = checkResult.Value.RoomId,
            IsDisabled = checkResult.Value.IsDisabled,
        });

        if (modifyResult.IsFailure) return BadRequest(modifyResult.Error);

        return Ok(modifyResult.Value);
    }

    [Authorize]
    [HttpPut("Cancel")]
    public async Task<IActionResult> CancelNote([FromQuery] Guid id)
    {
        var checkResult = await noteService.GetNoteIdAsync(id);

        if (checkResult.IsFailure) return NotFound(checkResult.Error);

        var modifyResult = await modifyNoteUseCase.ExecuteAsync(new()
        {
            Id = checkResult.Value.Id,
            CustomerId = checkResult.Value.CustomerId,
            CustomerName = checkResult.Value.CustomerName,
            CustomerPhone = checkResult.Value.CustomerPhone,
            Day = checkResult.Value.Day,
            Start = checkResult.Value.Start,
            End = checkResult.Value.End,
            PaidState = checkResult.Value.PaidState,
            RoomId = checkResult.Value.RoomId,
            IsDisabled = true
        });

        if (modifyResult.IsFailure) return BadRequest(modifyResult.Error);

        return Ok(modifyResult.Value);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> ChangeNote(ChangeNoteRequest request)
    {
        var checkResult = await noteService.GetNoteIdAsync(request.dto.Id);

        if (checkResult.IsFailure) return NotFound(checkResult.Error);

        var modifyResult = await modifyNoteUseCase.ExecuteAsync(request.dto);

        if (modifyResult.IsFailure) return BadRequest(modifyResult.Error);

        return Ok(modifyResult.Value);
    }

    [HttpDelete("DisableNote/{id}")]
    public async Task<IActionResult> DisableNote(Guid id)
    {
        var checkResult = await noteService.GetNoteIdAsync(id);

        if (checkResult.IsFailure) return NotFound(checkResult.Error);

        var modifyResult = await modifyNoteUseCase.ExecuteAsync(new()
        {
            Id = checkResult.Value.Id,
            CustomerId = checkResult.Value.CustomerId,
            CustomerName = checkResult.Value.CustomerName,
            CustomerPhone = checkResult.Value.CustomerPhone,
            Day = checkResult.Value.Day,
            Start = checkResult.Value.Start,
            End = checkResult.Value.End,
            PaidState = checkResult.Value.PaidState,
            IsDisabled = true,
        });

        if (modifyResult.IsFailure) return BadRequest(modifyResult.Error);

        return Ok(modifyResult.Value);
    }
}