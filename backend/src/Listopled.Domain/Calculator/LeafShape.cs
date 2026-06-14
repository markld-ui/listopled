namespace Listopled.Domain.Calculator;

using Listopled.Domain.Common;

public sealed class LeafShape : BaseEntity
{
    public LeafShape(
        string name,
        decimal surcharge,
        int sortOrder,
        string? description = null,
        bool isActive = true)
    {
        SetName(name);
        SetSurcharge(surcharge);
        SetSortOrder(sortOrder);
        Description = NormalizeOptional(description);
        IsActive = isActive;
    }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Surcharge { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }

    public void Update(string name, decimal surcharge, int sortOrder, string? description = null)
    {
        SetName(name);
        SetSurcharge(surcharge);
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
            throw new DomainException("Leaf shape name is required.");
        }

        Name = name.Trim();
    }

    private void SetSurcharge(decimal surcharge)
    {
        if (surcharge < 0)
        {
            throw new DomainException("Leaf shape surcharge cannot be negative.");
        }

        Surcharge = surcharge;
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
