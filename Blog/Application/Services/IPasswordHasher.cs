namespace Blog.API.Application.Services;

public interface IPasswordHasher
{
    string HashPasseword(string password);
    bool VerifyPassword(string storedHash, string password);
}
