using MediatR;

namespace Blog.API.Application.Commands.PostCommands.UpdatePostCommand;

public class UpdatePostCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
}
