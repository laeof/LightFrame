using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IRoomService {
    Task<IResult<Room>> GetRoomIdAsync(Guid id);
    Task<IResult<List<Room>>> GetAvailableRoomsAsync();
    Task<IResult<Room>> AddRoomAsync(Room room);
    Task<IResult<Room>> UpdateRoomAsync(Room room);
}