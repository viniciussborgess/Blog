using FluentValidation;

namespace Blog.API.Application.Commands.PostCommands.CreatePostCommand;

public class CreatePostCommandHandlerValidation : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandHandlerValidation()
    {
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
}
