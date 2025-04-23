using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository roomRepository;
    public RoomService(IRoomRepository roomRepository)
    {
        this.roomRepository = roomRepository;
    }
    public async Task<IResult<Room>> AddRoomAsync(Room room)
    {
        var result = await roomRepository.AddRoomAsync(room);

        return result;
    }

    public async Task<IResult<List<Room>>> GetAvailableRoomsAsync()
    {
        var result = await roomRepository.GetRoomsAsync();

        if (result.IsFailure) return Result<List<Room>>.Faillure(result.Error);

        return Result<List<Room>>.Success(result.Value.Where(r => !r.IsDisabled).ToList());
    }

    public async Task<IResult<Room>> GetRoomIdAsync(Guid id)
    {
        var result = await roomRepository.GetRoomByIdAsync(id);

        return result;
    }

    public async Task<IResult<Room>> UpdateRoomAsync(Room room)
    {
        var result = await roomRepository.UpdateRoomAsync(room);

        return result;
    }
}