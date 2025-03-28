using FluentValidation;

namespace Blog.API.Application.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommandHandlerValidation : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandHandlerValidation()
    {
        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MaximumLength(255)
            .WithMessage("O nome deve ter no maximo 255 caracteres");

        RuleFor(u => u.Senha)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("A senha deve ter pelo menos 8 caracteres.");
    }
}
