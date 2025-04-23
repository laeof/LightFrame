namespace Domain.Entities;

public class Photographer : EntityBase
{
    public string PhotoUrl { get; set; } = string.Empty;
    public string WorkExperience { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}