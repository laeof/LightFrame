namespace API.Requests;

public record AddNoteAsAuthorizedRequest(string Start, string End, string Day, string Name, string PhoneNumber, Guid RoomId, Guid Id) : AddNoteRequest(Start, End, Day, Name, PhoneNumber, RoomId);

public record AddNoteAsGuestRequest(string Start, string End, string Day, string Name, string PhoneNumber, Guid RoomId) : AddNoteRequest(Start, End, Day, Name, PhoneNumber, RoomId);

public record AddNoteRequest(string Start, string End, string Day, string Name, string PhoneNumber, Guid RoomId);