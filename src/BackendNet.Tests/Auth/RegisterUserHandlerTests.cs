using BackendNet.Application.Auth.Register;
using Moq;
using Xunit;
using BackendNet.Domain.Users;


namespace BackendNet.Tests.Auth;

public class RegisterUserHandlertest
{
    private readonly Mock<IUserRepository> _userRepositoryMock;

    private readonly Mock<IPasswordHasher> _passwordHasherMock;

    private readonly RegisterUserHandler _handler;

    public RegisterUserHandlertest()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _handler = new RegisterUserHandler(_userRepositoryMock.Object, _passwordHasherMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Throw_EmailAlreadyExistsException_When_Email_Exist()
    {
        var existingUser = new User(
            "Missael",
            "missael@gmail.com",
            "passwordHash",
            "Rondo"
        );

        var command = new RegisterUserCommand
        {
            FirstName = "Missael",
            LastName = "Rondo",
            Email = "missaelApaza@gmail.com",
            Password = "123456"
        };

        _userRepositoryMock.Setup(x => x.GetByEmailAsync(command.Email)).ReturnsAsync(existingUser);

        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _handler.Handle(command));

    }

    [Fact]
    public async Task Handle_Should_Create_User_When_Email_Does_Not_Exist()
    {
        _userRepositoryMock
            .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User?)null);

        _passwordHasherMock
            .Setup(x => x.Hash(It.IsAny<string>()))
            .Returns("HASH123");

        _userRepositoryMock
            .Setup(x => x.CreateAsync(It.IsAny<User>()))
            .ReturnsAsync((User user) => user);

        var command = new RegisterUserCommand
        {
            FirstName = "Missael",
            LastName = "Rondo",
            Email = "missael@gmail.com",
            Password = "123456"
        };

        var response = await _handler.Handle(command);

        Assert.Equal(command.FirstName, response.FirstName);
        Assert.Equal(command.LastName, response.LastName);
        Assert.Equal(command.Email, response.Email);

        _userRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once());
    }
}