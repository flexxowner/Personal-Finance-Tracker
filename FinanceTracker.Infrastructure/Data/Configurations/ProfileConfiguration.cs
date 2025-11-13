using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");

        builder.HasKey(p => p.ProfileId);

        builder.Property(p => p.DefaultCurrency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(p => p.TimeZone)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.DisplayName)
            .HasMaxLength(100);
    }
}
