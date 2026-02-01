using Microsoft.Extensions.DependencyInjection;

namespace FinanceTracker.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DI).Assembly));

        return services;
    }
}
