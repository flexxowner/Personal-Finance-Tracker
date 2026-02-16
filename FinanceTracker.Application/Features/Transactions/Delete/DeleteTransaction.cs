using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Transactions.Delete;

public static class DeleteTransaction
{
    public record Command(Guid Id) : IRequest<Result>;

    public class Handler(IAppDbContext dbContext) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var transaction = await dbContext.Transactions
                .FirstOrDefaultAsync(t => t.TransactionId == request.Id, cancellationToken);

            if (transaction is null)
            {
                return Result.Failure(new EntityNotFound<Transaction>(request.Id));
            }

            dbContext.Transactions.Remove(transaction);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
