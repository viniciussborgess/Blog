using Blog.Domain.Data.Models;

namespace Blog.Domain.Data.IRepositories;

public interface IUserRepository : IRepositoryBase<User, Guid>
{
    User GetByName(string name);
}
