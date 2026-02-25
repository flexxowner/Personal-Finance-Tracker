using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Transaction = FinanceTracker.Domain.Entities.Transaction;

namespace FinanceTracker.Application.Features.Transactions.Get;

public record GetTransaction(Guid Id, Guid UserId) : IRequest<Result<TransactionDto>>;
public record GetTransactions(Guid UserId) : IRequest<IReadOnlyCollection<TransactionDto>>;

public class Handler(IAppDbContext dbContext) : 
    IRequestHandler<GetTransaction, Result<TransactionDto>>,
    IRequestHandler<GetTransactions, IReadOnlyCollection<TransactionDto>>
{
    public async Task<Result<TransactionDto>> Handle(GetTransaction request, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TransactionId == request.Id && t.OwnerId == request.UserId, cancellationToken);

        if (transaction is null)
        {
            return Result.Failure<TransactionDto>(new EntityNotFound<Transaction>(request.Id));
        }

        return MapTransaction(transaction);
    }

    public async Task<IReadOnlyCollection<TransactionDto>> Handle(GetTransactions request, CancellationToken cancellationToken) 
        => await dbContext.Transactions
        .AsNoTracking()
        .Where(t => t.OwnerId == request.UserId)
        .Select(t => MapTransaction(t))
        .ToListAsync(cancellationToken);

    private static TransactionDto MapTransaction(Transaction transaction) => new(transaction.TransactionId)
    {
        Amount = transaction.Amount,
        OccurredAtUtc = transaction.OccurredAtUtc,
        ExchangeRate = transaction.ExchangeRate,
        Note = transaction.Note,
        TransactionCurrency = transaction.TransactionCurrency,
        Type = transaction.Type
    };
}
