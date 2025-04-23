using Application.Interfaces;

namespace Application.DTO;

public class Tokens : ITokens
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}