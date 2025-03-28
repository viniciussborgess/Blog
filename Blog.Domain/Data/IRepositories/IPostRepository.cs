using Blog.Domain.Data.Models;

namespace Blog.Domain.Data.IRepositories;

public interface IPostRepository : IRepositoryBase<Post, Guid>
{
}
