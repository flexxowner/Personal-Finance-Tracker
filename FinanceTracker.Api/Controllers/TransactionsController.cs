using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Features.Transactions.Create;
using FinanceTracker.Application.Features.Transactions.Delete;
using FinanceTracker.Application.Features.Transactions.Get;
using FinanceTracker.Application.Features.Transactions.Update;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransactionsController(IMediator mediator, ICurrentUserService currentUserService) : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateTransactionDto dto, CancellationToken cancellationToken)   
    {
        var command = dto.Adapt<CreateTransaction.Command>() with { OwnerId = currentUserService.UserId };
        var result = await mediator.Send(command, cancellationToken);

        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetById), new { id = result.Value}, result.Value)
            : HandleFailure(result);
    }

    [HttpGet("{transactionId:guid}")]
    public async Task<ActionResult> GetById(Guid transactionId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetTransaction(transactionId, currentUserService.UserId), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var transactions = await mediator.Send(new GetTransactions(currentUserService.UserId), cancellationToken);

        return Ok(transactions);
    }

    [HttpDelete("{transactionId:guid}")]
    public async Task<ActionResult> Delete(Guid transactionId, CancellationToken cancellationToken)
    {
        var deleteCommand = new DeleteTransaction.Command(transactionId);
        var result = await mediator.Send(deleteCommand, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpPut("{transactionId:guid}")]
    public async Task<ActionResult> Update(Guid transactionId, [FromBody] CreateTransactionDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<UpdateTransaction.Command>() with 
        { 
            OwnerId = currentUserService.UserId,
            TransactionId = transactionId 
        };

        var result = await mediator.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() :  HandleFailure(result);
    }
}
