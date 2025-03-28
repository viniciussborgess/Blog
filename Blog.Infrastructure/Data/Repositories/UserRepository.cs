using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDataContext context) : RepositoryBase<User, Guid>(context), IUserRepository
{
    public User GetByName(string name)
    {
        return _entity.FirstOrDefault(x => x.Nome == name);
    }

    public override List<User> GetAll()
    {

        var users = _entity.Include(u => u.Posts).ToList();
        return users;
    }
}

