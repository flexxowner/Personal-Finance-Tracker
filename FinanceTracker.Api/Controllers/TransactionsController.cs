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
    public async Task<ActionResult> Create([FromBody] CreateTransactionDto dto)   
    {
        var command = dto.Adapt<CreateTransaction.Command>() with { OwnerId = currentUserService.UserId };
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return HandleFailure(result);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value}, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetTransaction(id));

        if (!result.IsSuccess)
        {
            return HandleFailure(result);  
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveTransaction(Guid id)
    {
        var deleteCommand = new DeleteTransaction.Command(id);
        var result = await mediator.Send(deleteCommand);

        if (!result.IsSuccess)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpPut("{transactionId}")]
    public async Task<ActionResult> UpdateTransaction(Guid transactionId, [FromBody] CreateTransactionDto dto)
    {
        var command = dto.Adapt<UpdateTransaction.Command>() with 
        { 
            OwnerId = currentUserService.UserId,
            TransactionId = transactionId 
        };

        var result = await mediator.Send(command);
        if (!result.IsSuccess)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }
}
