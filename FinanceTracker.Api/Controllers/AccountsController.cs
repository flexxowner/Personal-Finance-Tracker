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
    public async Task<ActionResult> Create([FromBody] CreateAccountRequest account, CancellationToken cancellationToken)
    {
        var createResult = await accountService.CreateAsync(account, currentUserService.UserId, cancellationToken);

        return createResult.IsSuccess 
            ? CreatedAtAction(nameof(GetById), new { accountId = createResult.Value }, createResult.Value)
            : HandleFailure(createResult);
    }

    [HttpGet("{accountId:guid}")]
    public async Task<ActionResult> GetById(Guid accountId, CancellationToken cancellationToken)
    {
        var getResult = await accountService.GetByIdAsync(accountId, currentUserService.UserId, cancellationToken);

        return getResult.IsSuccess ? Ok(getResult.Value) : HandleFailure(getResult) ;
    }

    [HttpPut("{accountId:guid}")]
    public async Task<ActionResult> Update(Guid accountId, [FromBody] UpdateAccountRequest updateAccount, CancellationToken cancellationToken)
    {
        var result = await accountService.UpdateAsync(accountId, updateAccount, currentUserService.UserId, cancellationToken);

        return result.IsSuccess ? NoContent() : HandleFailure(result);
    }
}
