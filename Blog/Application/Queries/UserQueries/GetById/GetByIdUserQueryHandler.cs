using Blog.API.Application.Models.DTOs;
using Blog.Domain.Data.IRepositories;
using Blog.Domain.Data.Models;
using MediatR;

namespace Blog.API.Application.Queries.UserQueries.GetById;

public class GetByIdUserQueryHandler(IRepositoryBase<User, Guid> repository) : IRequestHandler<GetByIdUserQuery, UserDTO>
{
    private readonly IRepositoryBase<User, Guid> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public Task<UserDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = _repository.Get(request.Id);

        if (user is null)
        {
            return null;
        }

        return Task.FromResult(new UserDTO
        {
            Id = user.Id,
            Nome = user.Nome,
            Posts = user.Posts.Select(x => new PostDTO
            {
                Id = x.Id,
                UsuarioId = x.UsuarioId,
                Titulo = x.Titulo,
                Texto = x.Texto,
                DataPostagem = x.DataPostagem,
            }).ToList()
        });


    }
}
