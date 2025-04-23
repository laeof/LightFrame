namespace Application.DTO;

public class RoomDto
{
    public string Name = string.Empty;
    public string Address = string.Empty;
    public int Price = 0; 
    public List<string> PhotoUrl = new List<string>();
    public bool IsDisabled = false;
}