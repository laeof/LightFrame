using API.Requests;
using Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthenticateUserUseCase authenticateUserUseCase;
    private readonly RegisterUserUseCase registerUserUseCase;
    private readonly RefreshTokenUseCase refreshTokenUseCase;
    public AuthController(AuthenticateUserUseCase authenticateUserUseCase,
        RefreshTokenUseCase refreshTokenUseCase,
        RegisterUserUseCase registerUserUseCase)
    {
        this.authenticateUserUseCase = authenticateUserUseCase;
        this.refreshTokenUseCase = refreshTokenUseCase;
        this.registerUserUseCase = registerUserUseCase;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Register([FromBody]RegisterRequest request)
    {
        var userResult = await registerUserUseCase.ExecuteAsync(
            new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password,
                Phone = request.PhoneNumber,
                UserName = request.UserName
            });

        if (userResult.IsFailure) return Unauthorized(userResult.Error);

        var tokensResult = await authenticateUserUseCase.ExecuteAsync(request.Email, request.Password);

        if (tokensResult.IsFailure) return Unauthorized(new { tokensResult.Error });

        return Ok(tokensResult.Value);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Login([FromBody]LoginRequest request)
    {
        var tokensResult = await authenticateUserUseCase.ExecuteAsync(request.Email, request.Password);

        if (tokensResult.IsFailure) return Unauthorized(new { tokensResult.Error });

        return Ok(tokensResult.Value);
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        if (request.RefreshToken == "" || request.AccessToken == "")
            return BadRequest();

        var tokensResult = await refreshTokenUseCase.ExecuteAsync(request.RefreshToken);

        if (tokensResult.IsFailure) return Unauthorized(new { tokensResult.Error });

        return Ok(tokensResult.Value);
    }
}