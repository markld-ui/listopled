namespace Listopled.Domain.Calculator;

using Listopled.Domain.Common;

public sealed class Discount : BaseEntity
{
    public Discount(
        string name,
        decimal amount,
        int sortOrder,
        string? description = null,
        bool isActive = true)
    {
        SetName(name);
        SetAmount(amount);
        SetSortOrder(sortOrder);
        Description = NormalizeOptional(description);
        IsActive = isActive;
    }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Amount { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }

    public void Update(string name, decimal amount, int sortOrder, string? description = null)
    {
        SetName(name);
        SetAmount(amount);
        SetSortOrder(sortOrder);
        Description = NormalizeOptional(description);
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Discount name is required.");
        }

        Name = name.Trim();
    }

    private void SetAmount(decimal amount)
    {
        if (amount < 0)
        {
            throw new DomainException("Discount amount cannot be negative.");
        }

        Amount = amount;
    }

    private void SetSortOrder(int sortOrder)
    {
        if (sortOrder < 0)
        {
            throw new DomainException("Sort order cannot be negative.");
        }

        SortOrder = sortOrder;
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }
}
