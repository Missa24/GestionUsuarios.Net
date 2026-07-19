using BackendNet.Application.Auth.Register;
using BackendNet.Application.Auth.Login;
using BackendNet.Application.Auth.Dto;
using BackendNet.Application.Exceptions;
namespace BackendNet.Application.Auth.Login;

public class LoginUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public LoginUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }

    public async Task<LoginUserResponse> Handle(LoginUserCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            throw new InvalidCredentialsException();
        }
        var verify = _passwordHasher.Verify(command.Password, user.PasswordHash);
        if (!verify)
        {
            throw new InvalidCredentialsException();
        }
        var token = _tokenProvider.Generate(user);
        return new LoginUserResponse(user.Id, user.FirstName, user.LastName, user.Email, token);
    }
}