namespace Domain.Entities;

public class Note : EntityBase
{
    public string CustomerName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
}