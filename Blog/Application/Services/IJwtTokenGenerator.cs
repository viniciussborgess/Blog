using Blog.Domain.Data.Models;

namespace Blog.API.Application.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
