using Blog.Domain.Data.Core;

namespace Blog.Domain.Data.Models;

public class Post : Entity<Guid>
{
    private Post()
    {

    }
    public Post(Guid usuario, string titulo, string texto, DateTime dataPostagem)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuario;
        Titulo = titulo;
        Texto = texto;
        DataPostagem = dataPostagem;
    }

    public Guid UsuarioId { get; private set; }
    public User Usuario { get; private set; }
    public string Titulo { get; private set; }
    public string Texto { get; private set; }
    public DateTime DataPostagem { get; private set; }

    public void Update(string titulo, string texto)
    {
        Titulo = titulo;
        Texto = texto;
    }

}
