namespace FinanceTracker.Domain.Entities;

public sealed class Profile
{
    public Guid ProfileId { get; set; }

    public Guid OwnerId { get; set; }

    public string DefaultCurrency { get; set; } = string.Empty;

    public string TimeZone { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public User Owner { get; set; } = null!;
}
