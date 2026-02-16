using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Common.Interfaces.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}
