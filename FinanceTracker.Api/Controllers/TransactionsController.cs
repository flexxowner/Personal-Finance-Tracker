using FinanceTracker.Application.Features.Transactions.Create;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(IMediator mediator) : ControllerBase
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
        return Ok();
    }
}
