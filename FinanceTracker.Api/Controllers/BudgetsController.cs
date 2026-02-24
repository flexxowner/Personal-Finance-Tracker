using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Features.Budgets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BudgetsController(IBudgetService budgetService, ICurrentUserService currentUserService) : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await budgetService.GetAllAsync(currentUserService.UserId, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{budgetId:guid}")]
    public async Task<ActionResult> GetById(Guid budgetId, CancellationToken cancellationToken)
    {
        var result = await budgetService.GetByIdAsync(budgetId, currentUserService.UserId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateBudgetRequest createBudgetRequest, CancellationToken cancellationToken)
    {
        var result = await budgetService.CreateAsync(createBudgetRequest, currentUserService.UserId, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { budgetId = result.Value }, result.Value) 
            : HandleFailure(result);
    }

    [HttpPut("{budgetId:guid}")]
    public async Task<ActionResult> Update(Guid budgetId,[FromBody] UpdateBudgetRequest request, CancellationToken cancellationToken)
    {
        var result = await budgetService.UpdateAsync(budgetId, request, currentUserService.UserId, cancellationToken);
        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }

    [HttpDelete("{budgetId:guid}")]
    public async Task<ActionResult> Delete(Guid budgetId, CancellationToken cancellationToken)
    {
        var result = await budgetService.DeleteAsync(budgetId, currentUserService.UserId, cancellationToken);
        return result.IsSuccess ? NoContent() :  HandleFailure(result);
    }
}