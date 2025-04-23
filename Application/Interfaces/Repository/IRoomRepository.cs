using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IRoomRepository {
    Task<IResult<Room>> GetRoomByIdAsync(Guid id);
    Task<IResult<List<Room>>> GetRoomsAsync();
    Task<IResult<Room>> AddRoomAsync(Room room);
    Task<IResult<Room>> UpdateRoomAsync(Room room);
}