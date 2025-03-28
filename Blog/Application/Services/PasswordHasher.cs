namespace Blog.API.Application.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPasseword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string storedHash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}
