using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;
using MediatR;

namespace Blog.API.Application.Commands.PostCommands.CreatePostCommand;
public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Guid?>
{
    private readonly IPostRepository _repository;

    public CreatePostCommandHandler(IPostRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Guid?> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var post = new Post(
            request.UsuarioId,
            request.Titulo,
            request.Texto,
            DateTime.UtcNow
        );

        _repository.Create(post);

        if (!await _repository.SaveChangesAsync())
            return null;

        return post.Id;
    }

}
