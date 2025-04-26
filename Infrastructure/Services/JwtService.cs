using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common;
using Application.Interfaces;
using Application.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly string jwtKey = Environment.GetEnvironmentVariable("SECRETKEY")!;
    private readonly string issuer = Environment.GetEnvironmentVariable("ISSUER")!;
    private readonly string audience = Environment.GetEnvironmentVariable("AUDIENCE")!;
    public IResult<string> GenerateAccessToken(Guid userId, string email, IList<string> roles)
    {
        if (jwtKey is null) return Result<string>.Faillure(new("500", "Jwt key is null"));
        if (issuer is null) return Result<string>.Faillure(new("500", "Issuer is null"));
        if (audience is null) return Result<string>.Faillure(new("500", "Audience is null"));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userId.ToString()),
            new(ClaimTypes.Email, email),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return Result<string>.Success(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public IResult<string> GenerateRefreshToken()
    {
        var randomBytes = new byte[20];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Result<string>.Success(Convert.ToBase64String(randomBytes));
    }
}