namespace Domain.Entities;

public class Room : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Price { get; set; } = 0;
    public IList<string> PhotoUrl { get; set; } = new List<string>();
}