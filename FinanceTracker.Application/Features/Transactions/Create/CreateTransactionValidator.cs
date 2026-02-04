using FinanceTracker.Domain.Enums;
using FluentValidation;

namespace FinanceTracker.Application.Features.Transactions.Create;

public class CreateTransactionValidator : AbstractValidator<CreateTransactionDto>
{
    public CreateTransactionValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be positive")
            .LessThan(1_000_000_000);

        RuleFor(x => x.Currency)
            .Length(3)
            .WithMessage("Currency code must be 3 characters (e.g. USD)");

        RuleFor(x => x.Type).IsInEnum();

        RuleFor(x => x.AccountId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();

        RuleFor(x => x.Note)
            .MaximumLength(100)
            .When(x => x.Type == CategoryType.Expense);
    }
}
