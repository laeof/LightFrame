namespace Application.Interfaces;

public interface ITokens
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}