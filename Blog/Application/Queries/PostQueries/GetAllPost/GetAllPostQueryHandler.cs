using Blog.API.Application.Models.DTOs;
using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;
using MediatR;

namespace Blog.API.Application.Queries.PostQueries.GetAllPost;

public class GetAllPostQueryHandler(IRepositoryBase<Post, Guid> repository) : IRequestHandler<GetAllPostQuery, IEnumerable<PostDTO>>
{
    private readonly IRepositoryBase<Post, Guid> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public Task<IEnumerable<PostDTO>> Handle(GetAllPostQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var data = _repository.GetAll().Select(_ => new PostDTO
        {
            Id = _.Id,
            UsuarioId = _.UsuarioId,
            Titulo = _.Titulo,
            Texto = _.Texto,
            DataPostagem = _.DataPostagem,
        });

        return Task.FromResult(data);
    }
}
