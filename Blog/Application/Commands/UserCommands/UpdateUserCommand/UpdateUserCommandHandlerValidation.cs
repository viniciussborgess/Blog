using Blog.Domain.Data.IRepositories;
using FluentValidation;

namespace Blog.API.Application.Commands.UserCommands.UpdateUserCommand;

public class UpdateUserCommandHandlerValidation : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _repository;

    public UpdateUserCommandHandlerValidation(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        RuleFor(x => x.Id)
           .NotEmpty()
          .Must(ToExistUser)
          .WithMessage("O User deve existir no banco de dados");

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

    private bool ToExistUser(Guid UserId)
    {
        var result = _repository.Get(UserId);

        if (result == null)
        {
            return false;
        }
        return true;
    }
}
