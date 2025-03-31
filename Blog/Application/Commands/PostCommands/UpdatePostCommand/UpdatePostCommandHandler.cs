using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Commands.PostCommands.UpdatePostCommand;

public class UpdatePostCommandHandler: IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IPostRepository _repository;

    public UpdatePostCommandHandler(IPostRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var post = _repository.Get(request.Id) ?? throw new NullReferenceException();

        if (post.UsuarioId != request.UsuarioId)
            return false;

        post.Update(request.Titulo, request.Texto);

        return await _repository.SaveChangesAsync();
    }
}
