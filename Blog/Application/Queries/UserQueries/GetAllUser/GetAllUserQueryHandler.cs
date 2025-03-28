using Blog.API.Application.Models.DTOs;
using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Queries.UserQueries.GetAllUser;

public class GetAllUserQueryHandler(IUserRepository repository) : IRequestHandler<GetAllUserQuery, IEnumerable<UserDTO>>
{
    private readonly IUserRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public Task<IEnumerable<UserDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var data = _repository.GetAll().Select(_ => new UserDTO
        {
            Id = _.Id,
            Nome = _.Nome,
            Posts = _.Posts.Select(x => new PostDTO
            {
                Id = x.Id,
                UsuarioId = x.UsuarioId,
                Titulo = x.Titulo,
                Texto = x.Texto,
                DataPostagem = x.DataPostagem,
            }).ToList()
        });

        return Task.FromResult(data);
    }
}
