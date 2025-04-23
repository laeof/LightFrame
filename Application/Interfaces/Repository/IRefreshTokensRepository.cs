using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IRefreshTokensRepository {
    Task<IResult<UserRefreshToken>> GetRefreshTokenByUserIdAsync(Guid id);
    Task<IResult<Guid>> GetUseridWithTokenAsync(string token);
    Task<IResult<UserRefreshToken>> CreateRefreshTokenAsync(Guid userId, string refreshToken);
    Task<IResult<bool>> RevokeRefreshTokenAsync(string token);
}