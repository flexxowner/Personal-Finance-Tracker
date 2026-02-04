using FinanceTracker.Domain.Shared;
using FluentValidation;
using MediatR;

namespace FinanceTracker.Application.Validation;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next(cancellationToken);
        }

        var context = new ValidationContext<TRequest>(request);
        var validationsResult = await Task.WhenAll(
            validators.Select(x => x.ValidateAsync(context, cancellationToken)));

        var failures = validationsResult
            .SelectMany(x => x.Errors)
            .Where(f => f != null);

        if (failures.Any())
        {
            var validationError = new ResultError(ErrorType.Validation, $"Failed to validate {request}");
            var errorMessages = failures.Select(x => x.ErrorMessage).ToArray();

            return TResponse.Failure(validationError with { ErrorMessages = errorMessages });
        }

        return await next(cancellationToken);
    }
}
