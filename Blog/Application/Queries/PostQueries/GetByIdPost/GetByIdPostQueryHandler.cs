using Blog.API.Application.Models.DTOs;
using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;
using MediatR;

namespace Blog.API.Application.Queries.PostQueries.GetByIdPost;

public class GetByIdPostQueryHandler(IRepositoryBase<Post, Guid> repository) : IRequestHandler<GetByIdPostQuery, PostDTO>
{
    private readonly IRepositoryBase<Post, Guid> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public Task<PostDTO> Handle(GetByIdPostQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var post = _repository.Get(request.Id);

        if (post is null)
        {
            return null;
        }

        return Task.FromResult(new PostDTO
        {
            Id = post.Id,
            UsuarioId = post.UsuarioId,
            Titulo = post.Titulo,
            Texto = post.Texto,
            DataPostagem = post.DataPostagem,
        });


    }
}
