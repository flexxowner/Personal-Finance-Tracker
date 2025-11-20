using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Infrastructure.Data.Configurations;

internal class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(b => b.BudgetId);

        builder.Property(b => b.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(b => b.LimitAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.IsDeleted)
            .HasDefaultValue(false);

        builder.Property(b => b.CreatedUtc)
            .HasDefaultValueSql("NOW()");

        builder.Property(b => b.PeriodStart)
            .IsRequired();
        builder.Property(b => b.PeriodEnd)
            .IsRequired();

        builder.Property(b => b.UpdatedUtc);

        builder.HasIndex(b => b.OwnerId);
        builder.HasIndex(b => b.CategoryId);

        builder.HasQueryFilter(b => !b.IsDeleted);

        builder.HasOne(b => b.Owner)
            .WithMany(b => b.Budgets)
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);            
    }
}
