using Application.DTO;

namespace API.Requests;

public record UpdateRoomRequest(Guid RoomId, RoomDto Dto);