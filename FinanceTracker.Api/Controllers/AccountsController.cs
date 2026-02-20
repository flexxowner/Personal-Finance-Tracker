using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Features.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountsController(
    IAccountService accountService,
    ICurrentUserService currentUserService)
    : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var accounts = await accountService.GetAllAsync(currentUserService.UserId, cancellationToken);

        return Ok(accounts);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateAccountDto account, CancellationToken cancellationToken)
    {
        var createResult = await accountService.CreateAsync(account, currentUserService.UserId, cancellationToken);

        if (!createResult.IsSuccess)
        {
            return HandleFailure(createResult);
        }

        return CreatedAtAction(nameof(GetById), new { accountId = createResult.Value }, createResult.Value);
    }

    [HttpGet("{accountId:guid}")]
    public async Task<ActionResult> GetById(Guid accountId, CancellationToken cancellationToken)
    {
        var getResult = await accountService.GetByIdAsync(accountId, currentUserService.UserId, cancellationToken);

        if (!getResult.IsSuccess)
        {
            return HandleFailure(getResult);
        }

        return Ok(getResult.Value);
    }

    [HttpPut("{accountId:guid}")]
    public async Task<ActionResult> Update(Guid accountId, [FromBody] UpdateAccountDto updateAccount, CancellationToken cancellationToken)
    {
        var result = await accountService.UpdateAsync(accountId, updateAccount, currentUserService.UserId, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleFailure(result);
        }

        return NoContent();
    }

    [HttpDelete("{accountId:guid}")]
    public async Task<ActionResult> Delete(Guid accountId, CancellationToken cancellationToken)
    {
        var result = await accountService.RemoveAsync(accountId, currentUserService.UserId, cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleFailure(result);
        }
        
        return NoContent();
    }
}
