namespace BackendNet.Application.Auth.Dto;

public record RegisterUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);