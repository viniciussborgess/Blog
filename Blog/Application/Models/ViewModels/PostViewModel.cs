namespace Blog.API.Application.Models.ViewModels;

public class PostViewModel
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public DateTime DataPostagem { get; set; }
}
