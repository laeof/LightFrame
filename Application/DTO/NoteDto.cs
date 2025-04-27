namespace Application.DTO;

public class NoteDto
{
    public Guid Id { get; set; }
    public bool IsDisabled { get; set; } = false;
    public string Start { get; set; } = string.Empty;
    public string End { get; set; } = string.Empty;
    public string Day { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public bool PaidState { get; set; } = false;
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
}