using FinanceTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Application.Features.Transactions.Create;

public record CreateTransactionDto
{
    public Guid? BudgetId { get; init; }

    [Required]
    public Guid CategoryId { get; init; }

    [Required]
    public Guid AccountId { get; init; }

    [Required]
    [Range(0.01, 10000000, ErrorMessage = "The amount must be greater than 0")]
    public decimal Amount { get; init; }

    [Required]
    public DateTime OccurredAtUtc { get; init; }

    [Required]
    [EnumDataType(typeof(CategoryType))]
    public CategoryType Type { get; init; }

    [Range(0.000001, 1000000)]
    public decimal ExchangeRate { get; init; } = 1.0m;

    [StringLength(3, MinimumLength = 3, ErrorMessage = "Use the ISO currency code (USD, RUB, EUR)")]
    public required string Currency { get; init; }

    [MaxLength(500)]
    public string? Note { get; init; }
}
