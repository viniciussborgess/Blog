using MediatR;

namespace Blog.API.Application.Commands.UserCommands.UpdateUserCommand;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; }
}
