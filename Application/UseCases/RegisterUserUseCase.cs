using Application.Common;
using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.UseCases;

public class RegisterUserUseCase
{
    private readonly IUserService userService;
    private readonly IPasswordHasher passwordHasher;
    public RegisterUserUseCase(IUserService userService, IPasswordHasher passwordHasher)
    {
        this.userService = userService;
        this.passwordHasher = passwordHasher;
    }

    public async Task<IResult<User>> ExecuteAsync(Register dto)
    {
        var userExistsResult = await userService.ExistsByEmailAsync(dto.Email);

        if (userExistsResult.IsSuccess) return Result<User>.Faillure(new("302", "User exists"));

        var passwordHashResult = passwordHasher.Hash(dto.Password);

        if (passwordHashResult.IsFailure) return Result<User>.Faillure(passwordHashResult.Error);

        var userResult = await userService.RegisterUserAsync(dto, passwordHashResult.Value);

        if(userResult.IsFailure) return Result<User>.Faillure(userResult.Error);

        return userResult;
    }
}