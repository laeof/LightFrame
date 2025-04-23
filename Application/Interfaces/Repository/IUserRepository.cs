using Domain.Entities;

namespace Application.Interfaces.Repository;

public interface IUserRepository {
    Task<IResult<User>> GetUserByIdAsync(Guid id);
    Task<IResult<User>> GetUserByEmailAsync(string email);
    Task<IResult<User>> CreateUserAsync(User user);
    Task<IResult<User>> ModifyUserAsync(User user);
    Task<IResult<bool>> DisableUserAsync(Guid userId);
}