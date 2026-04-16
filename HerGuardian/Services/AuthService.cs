using HerGuardian.Models;
using HerGuardian.Services;

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Register(string name, string email, string password)
    {
        var exists = _context.Users.Any(u => u.Email == email);
        if (exists) return false;

        var user = new User
        {
            Name = name,
            Email = email,
            Password = PasswordHelper.HashPassword(password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public User Login(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);

        if (user == null) return null;

        var isValid = PasswordHelper.VerifyPassword(password, user.Password);

        return isValid ? user : null;
    }
}