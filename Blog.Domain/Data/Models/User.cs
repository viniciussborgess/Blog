using Blog.Domain.Data.Core;

namespace Blog.Domain.Data.Models;

public class User : Entity<Guid>
{
    private User()
    {

    }
    public User(string nome, string senha)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Senha = senha;
        Posts = new List<Post>();
    }

    public string Nome { get; private set; }
    public string Senha { get; private set; }
    public List<Post> Posts { get; private set; }

    public void Update(string nome, string senha)
    {
        Nome = nome;
        Senha = senha;
    }

    public void AddPost(Post post)
    {
        if (post == null) throw new ArgumentNullException(nameof(post));

        Posts.Add(post);
    }
}
