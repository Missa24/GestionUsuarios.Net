using BackendNet.Application.Auth.Register;
using BackendNet.Domain.Users;
using BackendNet.Infrastructure.Persistence;
using BackendNet.Application.Users.Command;
using Microsoft.EntityFrameworkCore;
namespace BackendNet.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(user =>
                user.Id == id &&
                user.IsActive
            );
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string> DeleteUserAsync()
    {
        await _context.SaveChangesAsync();

        return "usuario desactivated.";
    }

    public async Task<List<User>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Users
            .Where(user => user.IsActive)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<int> CountAsync()
    {
        return await _context.Users.CountAsync();
    }

}