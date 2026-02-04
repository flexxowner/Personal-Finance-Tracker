using FinanceTracker.Application.Features.Transactions.Create;
using FinanceTracker.Application.Features.Transactions.Delete;
using FinanceTracker.Application.Features.Transactions.Get;
using FinanceTracker.Application.Features.Transactions.Update;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(IMediator mediator) : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)   
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //TODO: Change after adding Auth 
        var command = dto.Adapt<CreateTransaction.Command>() with { OwnerId = userId };
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
        {
            return HandleFailure(result.Error);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value}, result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetTransaction(id));

        if (!result.IsSuccess)
        {
            return HandleFailure(result.Error);  
        }

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveTransaction(Guid id)
    {
        var deleteCommand = new DeleteTransaction.Command(id);
        var result = await mediator.Send(deleteCommand);

        if (!result.IsSuccess)
        {
            return HandleFailure(result.Error);
        }

        return NoContent();
    }

    [HttpPut("{transactionId}")]
    public async Task<IActionResult> UpdateTransaction(Guid transactionId, [FromBody] CreateTransactionDto dto)
    {
        var userId = Guid.Parse("11111111-1111-1111-1111-111111111111"); //TODO: Change after adding Auth 
        var command = dto.Adapt<UpdateTransaction.Command>() with 
        { 
            OwnerId = userId,
            TransactionId = transactionId 
        };

        var result = await mediator.Send(command);
        if (!result.IsSuccess)
        {
            return HandleFailure(result.Error);
        }

        return NoContent();
    }
}
