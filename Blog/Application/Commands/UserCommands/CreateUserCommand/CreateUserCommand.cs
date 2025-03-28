using MediatR;

namespace Blog.API.Application.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommand : IRequest<Guid?>
{
    public string Nome { get; set; }
    public string Senha { get; set; }
}
