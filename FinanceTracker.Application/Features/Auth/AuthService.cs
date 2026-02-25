using FinanceTracker.Application.Common.Interfaces;
using FinanceTracker.Application.Common.Interfaces.Authentication;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Application.Features.Auth;

public class AuthService(
    IAppDbContext dbContext,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : IAuthService
{
    public async Task<Result<string>> RegisterAsync(AuthCredentialsDto authCredentials, CancellationToken cancellationToken = default)
    {
        var email = authCredentials.Email;
        var emailExists = await dbContext.Users.AnyAsync(u => u.Email == email, cancellationToken);

        if (emailExists)
        {
            return Result.Failure<string>(new ResultError(ErrorType.Conflict, "User with this email already exists"));
        }

        var passwordHash = passwordHasher.Hash(authCredentials.Password);

        var user = new User(email: email, passwordHash: passwordHash, DateTime.UtcNow);

        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return jwtProvider.Generate(user);
    }

    public async Task<Result<string>> LoginAsync(AuthCredentialsDto authCredentials, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == authCredentials.Email, cancellationToken);
        if (user is null)
        {
            return Result.Failure<string>(new ResultError(ErrorType.Validation, "Invalid email"));
        }

        var isPasswordValid = passwordHasher.Verify(authCredentials.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return Result.Failure<string>(new ResultError(ErrorType.Validation, "Invalid password"));
        }

        return jwtProvider.Generate(user);
    }
}
