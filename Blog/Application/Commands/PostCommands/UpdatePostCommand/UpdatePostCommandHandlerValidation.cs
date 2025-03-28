using Blog.Domain.Data.IRepositories;
using FluentValidation;

namespace Blog.API.Application.Commands.PostCommands.UpdatePostCommand;

public class UpdatePostCommandHandlerValidation : AbstractValidator<UpdatePostCommand>
{
    private readonly IPostRepository _repository;

    public UpdatePostCommandHandlerValidation(IPostRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        RuleFor(x => x.Id)
            .NotEmpty()
            .Must(ToExistPost)
            .WithMessage("O Post deve existir no banco de dados");

        RuleFor(p => p.UsuarioId)
           .NotEmpty()
           .WithMessage("Usuário é obrigatório.");

        RuleFor(p => p.Titulo)
            .NotEmpty()
            .WithMessage("O título é obrigatório.")
            .MaximumLength(255)
            .WithMessage("O Titulo não pode ter mais de 255 caracteres");

        RuleFor(p => p.Texto)
            .NotEmpty()
            .WithMessage("O texto do post é obrigatório.");
    }

    private bool ToExistPost(Guid postId)
    {
        var result = _repository.Get(postId);

        if (result == null)
        {
            return false;
        }
        return true;
    }
}
