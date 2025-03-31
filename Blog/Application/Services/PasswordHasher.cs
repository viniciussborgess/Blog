namespace Blog.API.Application.Services;

public class PasswordHasher : IPasswordHasher
{
    private string salt = BCrypt.Net.BCrypt.GenerateSalt();
    public string HashPasseword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, salt);
    }

    public bool VerifyPassword(string storedHash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}
