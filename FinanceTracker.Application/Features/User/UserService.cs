using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Domain.Shared;
using FinanceTracker.Domain.Shared.Errors;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.User;

public class UserService(IAppDbContext dbContext) : IUserService
{
    public async Task<Result<UserDto>> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(new EntityNotFound<Domain.Entities.User>(userId));
        }

        return new UserDto(
            user.UserId, user.Email, user.Profile.DisplayName, user.Profile.DefaultCurrency)
        {
            Balance = 0
        };
    }
}