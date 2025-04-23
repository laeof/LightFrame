namespace Application.Interfaces.Services;

public interface IPasswordHasher
{
    IResult<string> Hash(string password);
    IResult<bool> Verify(string password, string hashedPassword);
}