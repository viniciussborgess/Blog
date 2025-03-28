using Blog.API.Application.Services;
using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;
using MediatR;

namespace Blog.API.Application.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid?>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async Task<Guid?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var password = _passwordHasher.HashPasseword(request.Senha);

        var user = new User
            (
                request.Nome,
                password
            );

        _repository.Create(user);

        if (!await _repository.SaveChangesAsync())
        {
            return null;
        }

        return user.Id;
    }
}
