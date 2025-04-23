using Domain.Entities;

namespace Application.Interfaces.Services;

public interface IAuthService {
    Task<IResult<User>> AuthenticateAsync(string email, string password);
    Task<IResult<UserRefreshToken>> AddRefreshTokenAsync(Guid userId, string refreshToken);
}