using FinanceTracker.Application.Common.Interfaces;
using System.Security.Claims;

namespace FinanceTracker.Api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (claim is null || claim.Value is not string id)
            {
                return Guid.Empty;
            }

            return Guid.Parse(id);
        }
    }
}
