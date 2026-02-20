using FinanceTracker.Application.Features.Accounts;
using FinanceTracker.Application.Features.Auth;
using FinanceTracker.Application.Features.Transactions.Create;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DI).Assembly));
        services.AddValidatorsFromAssemblyContaining<CreateTransactionValidator>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}
