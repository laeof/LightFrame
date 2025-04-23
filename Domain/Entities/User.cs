namespace Domain.Entities;

public class User : EntityBase
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public IList<Role> Roles { get; init; } = new List<Role>();
    public IList<UserRefreshToken> UserRefreshTokens { get; init; } = new List<UserRefreshToken>();
}