using System.ComponentModel;
using System.Security.Cryptography;

namespace Domain.Entities;

public class Note : EntityBase
{
    public string Start { get; set; } = string.Empty;
    public string End { get; set; } = string.Empty;
    public string Day { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;
    public bool PaidState { get; set; } = false;
    public Guid CustomerId { get; set; }
    public Guid RoomId { get; set; }
}