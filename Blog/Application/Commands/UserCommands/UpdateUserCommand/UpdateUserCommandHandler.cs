using Blog.API.Application.Services;
using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Commands.UserCommands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private IUserRepository _repository;
    private IPasswordHasher _passwordHasher;
    public UpdateUserCommandHandler(IUserRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = _repository.Get(request.Id) ?? throw new NullReferenceException();

        var hashpassword = _passwordHasher.HashPasseword(request.Senha);

        user.Update(request.Nome, hashpassword);

        return await _repository.SaveChangesAsync();
    }
}
