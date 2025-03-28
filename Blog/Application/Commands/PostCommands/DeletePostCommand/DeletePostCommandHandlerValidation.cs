using Blog.Domain.Data.IRepositories;
using FluentValidation;

namespace Blog.API.Application.Commands.PostCommands.DeletePostCommand;

public class DeletePostCommandHandlerValidation : AbstractValidator<DeletePostCommand>
{
    private readonly IPostRepository _repository;
    public DeletePostCommandHandlerValidation(IPostRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        RuleFor(x => x.Id)
            .NotEmpty()
           .Must(ToExistPost)
           .WithMessage("O Post deve existir no banco de dados");

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
