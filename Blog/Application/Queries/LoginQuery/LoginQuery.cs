using MediatR;

namespace Blog.API.Application.Queries.LoginQuery;

public class LoginQuery : IRequest<LoginReponse>
{
    public string Nome { get; set; }
    public string Senha { get; set; }
}
