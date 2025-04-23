namespace Domain.Entities;

public class UserRefreshToken : EntityBase
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expires { get; set; }
    public bool isRevoked { get; set; }
}