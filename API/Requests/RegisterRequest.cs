namespace API.Requests;

public record RegisterRequest(string FirstName,
    string LastName,
    string PhoneNumber,
    string UserName,
    string Email,
    string Password);