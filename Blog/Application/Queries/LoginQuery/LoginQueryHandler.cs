using Blog.API.Application.Services;
using Blog.Domain.Data.Exceptions;
using Blog.Domain.Data.IRepositories;
using MediatR;

namespace Blog.API.Application.Queries.LoginQuery;

public class LoginQueryHandler(IUserRepository repository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginQuery, LoginReponse>
{
    private readonly IUserRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IPasswordHasher _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator ?? throw new ArgumentNullException(nameof(jwtTokenGenerator));

    public Task<LoginReponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var user = _repository.GetByName(request.Nome);

        if (user is null)
        {
            throw new UserDomainException("Usuário inválido");
        }

        if (!_passwordHasher.VerifyPassword(user.Senha, request.Senha))
        {
            throw new UserDomainException("Senha inválido");
        }

        return Task.FromResult(new LoginReponse
        {
            Token = _jwtTokenGenerator.GenerateToken(user),
        });
    }
}
