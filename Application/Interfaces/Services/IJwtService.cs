namespace Application.Interfaces.Services;

public interface IJwtService {
    IResult<string> GenerateAccessToken(Guid userId, string email, IList<string> roles);
    IResult<string> GenerateRefreshToken();
}