namespace API.Requests;

public record RefreshTokenRequest(string AccessToken, string RefreshToken);