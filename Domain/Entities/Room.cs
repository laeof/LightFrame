namespace Domain.Entities;

public class Room : EntityBase
{
    public string Name = string.Empty;
    public string Address = string.Empty;
    public int Price = 0;
    public IList<string> PhotoUrl = new List<string>();
}