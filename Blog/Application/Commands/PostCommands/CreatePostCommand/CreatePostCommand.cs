using MediatR;

namespace Blog.API.Application.Commands.PostCommands.CreatePostCommand;

public class CreatePostCommand : IRequest<Guid?>
{
    public Guid UsuarioId { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
}
