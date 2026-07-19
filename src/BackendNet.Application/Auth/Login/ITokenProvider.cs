using BackendNet.Domain.Users;

namespace BackendNet.Application.Auth.Login;

public interface ITokenProvider
{
    string Generate(User user);
}