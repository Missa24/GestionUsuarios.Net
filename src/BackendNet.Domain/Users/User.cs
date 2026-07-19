namespace BackendNet.Domain.Users;

public class User
{
    public Guid Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; private set; }

    public User(string firstName, string email, string passwordHash, string lastName)
    {
        this.FirstName = firstName;
        this.Email = email;
        this.PasswordHash = passwordHash;
        this.LastName = lastName;
        this.Id = Guid.NewGuid();
        this.IsActive = true;
        this.CreatedAt = DateTime.UtcNow;
        this.UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string? firstName, string? lastName, string? email)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
            FirstName = firstName;

        if (!string.IsNullOrWhiteSpace(lastName))
            LastName = lastName;

        if (!string.IsNullOrWhiteSpace(email))
            Email = email;

        this.UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        this.IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

}

