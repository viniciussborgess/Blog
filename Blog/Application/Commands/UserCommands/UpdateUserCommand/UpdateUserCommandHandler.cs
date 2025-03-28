using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Commands.UserCommands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private IUserRepository _repository;
    public UpdateUserCommandHandler(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = _repository.Get(request.Id) ?? throw new NullReferenceException();

        user.Update(request.Nome, request.Senha);

        return await _repository.SaveChangesAsync();
    }
}
