using MediatR;

namespace Blog.API.Application.Commands.PostCommands.DeletePostCommand;

public class DeletePostCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
