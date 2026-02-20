namespace FinanceTracker.Domain.Entities;

public sealed class Profile
{
    private Profile() { }

    public Profile(Guid ownerId, string defaultCurrency, string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(defaultCurrency)) throw new ArgumentException("Currency required");

        ProfileId = Guid.NewGuid();
        OwnerId = ownerId;
        DefaultCurrency = defaultCurrency;
        DisplayName = displayName;
        TimeZone = TimeZoneInfo.Utc.DisplayName;
    }

    public Guid ProfileId { get; private set; }

    public Guid OwnerId { get; private set; }

    public string DefaultCurrency { get; private set; } = string.Empty;

    public string TimeZone { get; private set; } = string.Empty;

    public string DisplayName { get; private set; } = string.Empty;

    public User Owner { get; private set; } = null!;
}
