namespace BackendNet.Application.Auth.Dto;

public record LoginUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);