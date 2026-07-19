namespace BackendNet.Application.Users.Dto;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);
