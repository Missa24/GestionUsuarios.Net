using BackendNet.Domain.Users;
namespace BackendNet.Application.Auth.Register;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);

    Task<User> CreateAsync(User user);

    Task<User?> GetByEmailAsync(string email);

    Task<User> UpdateUserAsync(User user);

    Task<string> DeleteUserAsync();

    Task<List<User>> GetAllAsync(int page, int pageSize);

    Task<int> CountAsync();
}