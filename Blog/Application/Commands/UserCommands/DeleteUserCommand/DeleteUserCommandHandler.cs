using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Commands.UserCommands.DeleteUserCommand;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserRepository _repository;
    public DeleteUserCommandHandler(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = _repository.Get(request.Id);

        _repository.Remove(user);

        return await _repository.SaveChangesAsync();
    }
}
