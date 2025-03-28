using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;

namespace Blog.Infrastructure.Data.Repositories;

public class PostRepository(ApplicationDataContext context) : RepositoryBase<Post, Guid>(context), IPostRepository
{
}
