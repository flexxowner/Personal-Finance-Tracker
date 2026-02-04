using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using FinanceTracker.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace FinanceTracker.Application.Features.Transactions.Get;

public record GetTransaction(Guid Id) : IRequest<Result<TransactionDto>>;

public class Handler(AppDbContext dbContext) : IRequestHandler<GetTransaction, Result<TransactionDto>>
{
    public async Task<Result<TransactionDto>> Handle(GetTransaction request, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TransactionId == request.Id, cancellationToken);

        if (transaction is null)
        {
            return Result.Failure<TransactionDto>(new EntityNotFound<Transaction>(request.Id));
        }

        return new TransactionDto(transaction.TransactionId)
        {
            Amount = transaction.Amount,
            OccurredAtUtc = transaction.OccurredAtUtc,
            ExchangeRate = transaction.ExchangeRate,
            Note = transaction.Note,
            TransactionCurrency = transaction.TransactionCurrency,
            Type = transaction.Type
        };
    }
}
