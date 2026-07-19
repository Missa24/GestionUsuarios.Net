using BackendNet.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BackendNet.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}