using Blog.Domain.Data.Exceptions;
using FluentValidation;
using MediatR;

namespace Blog.API.Application.Behaviors;

public class ValidatorBehavior<TRequest, TReponse> : IPipelineBehavior<TRequest, TReponse> where TRequest : IRequest<TReponse>
{
    private const string Message = "Validation error";
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
    public async Task<TReponse> Handle(TRequest request, RequestHandlerDelegate<TReponse> next, CancellationToken cancellationToken)
    {
        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new BlogDomainException($"Command Validation Errors for type {typeof(TRequest).Name}", new ValidationException(Message, failures));
        }

        return await next();
    }
}
