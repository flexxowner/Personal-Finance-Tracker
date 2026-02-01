using FinanceTracker.Application.Features.Transactions.Create;
using FinanceTracker.Application.Features.Transactions.Get;
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
        return CreatedAtAction(nameof(GetById), new { id = result}, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetTransactionQuery(id));

        if (!result.IsSuccess)
        {
            return HandleFailure(result.Error);  
        }

        return Ok(result.Value);
    }
}
