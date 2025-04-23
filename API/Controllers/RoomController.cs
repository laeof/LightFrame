using API.Requests;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class RoomController : ControllerBase
{
    public IRoomService roomService;
    public RoomController(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var result = await roomService.GetAvailableRoomsAsync();

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(Guid id)
    {
        var result = await roomService.GetRoomIdAsync(id);

        if (result.IsFailure) return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddRoom(AddRoomRequest request)
    {
        var result = await roomService.AddRoomAsync(new()
        {
            Address = request.Dto.Address,
            Name = request.Dto.Name,
            PhotoUrl = request.Dto.PhotoUrl,
            Price = request.Dto.Price,
        });

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> UpdateRoom(UpdateRoomRequest request)
    {
        var result = await roomService.UpdateRoomAsync(new()
        {
            Address = request.Dto.Address,
            Name = request.Dto.Name,
            IsDisabled = request.Dto.IsDisabled,
            PhotoUrl = request.Dto.PhotoUrl,
            Price = request.Dto.Price,
        });

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("Disable")]
    public async Task<IActionResult> DisableRoom(Guid id)
    {
        var findResult = await roomService.GetRoomIdAsync(id);

        if (findResult.IsFailure) return NotFound(findResult.Error);

        var result = await roomService.UpdateRoomAsync(new()
        {
            Id = id,
            Address = findResult.Value.Address,
            Name = findResult.Value.Name,
            PhotoUrl = (List<string>)findResult.Value.PhotoUrl,
            Price = findResult.Value.Price,
            IsDisabled = true
        });

        if (result.IsFailure) return BadRequest(result.Error);

        return Ok(result.Value);
    }
}