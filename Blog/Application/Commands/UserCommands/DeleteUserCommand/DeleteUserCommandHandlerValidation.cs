using Blog.Domain.Data.IRepositories;
using FluentValidation;

namespace Blog.API.Application.Commands.UserCommands.DeleteUserCommand;

public class DeleteUserCommandHandlerValidation : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserRepository _repository;

    public DeleteUserCommandHandlerValidation(IUserRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        RuleFor(x => x.Id)
           .NotEmpty()
          .Must(ToExistUser)
          .WithMessage("O Post deve existir no banco de dados");

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
