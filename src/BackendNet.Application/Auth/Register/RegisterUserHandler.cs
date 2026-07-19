using BackendNet.Application.Auth.Register;
using BackendNet.Application.Auth.Dto;
using BackendNet.Domain.Users;
namespace BackendNet.Application.Auth.Register;

public class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand command)
    {
        var existingUser = await _userRepository.GetByEmailAsync(command.Email);
        if (existingUser is not null)
        {
            throw new EmailAlreadyExistsException();
        }
        var passwordHash = _passwordHasher.Hash(command.Password);

        var user = new User(command.FirstName, command.Email, passwordHash, command.LastName);
        await _userRepository.CreateAsync(user);

        return new RegisterUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email
        );
    }

    


}
