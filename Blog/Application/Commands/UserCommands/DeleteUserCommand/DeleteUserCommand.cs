using MediatR;

namespace Blog.API.Application.Commands.UserCommands.DeleteUserCommand;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
