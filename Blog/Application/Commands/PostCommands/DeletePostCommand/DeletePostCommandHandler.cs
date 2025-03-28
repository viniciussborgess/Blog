using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Commands.PostCommands.DeletePostCommand;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _repository;

    public DeletePostCommandHandler(IPostRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var post = _repository.Get(request.Id);

        _repository.Remove(post);

        return await _repository.SaveChangesAsync();
    }
}
