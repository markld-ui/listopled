namespace Listopled.Domain.Calculator;

using Listopled.Domain.Common;

public sealed class Fabric : BaseEntity
{
    public Fabric(
        string name,
        decimal pricePerUnit,
        int sortOrder,
        string? description = null,
        bool isActive = true)
    {
        SetName(name);
        SetPricePerUnit(pricePerUnit);
        SetSortOrder(sortOrder);
        Description = NormalizeOptional(description);
        IsActive = isActive;
    }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }

    public void Update(string name, decimal pricePerUnit, int sortOrder, string? description = null)
    {
        SetName(name);
        SetPricePerUnit(pricePerUnit);
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
            throw new DomainException("Fabric name is required.");
        }

        Name = name.Trim();
    }

    private void SetPricePerUnit(decimal pricePerUnit)
    {
        if (pricePerUnit < 0)
        {
            throw new DomainException("Fabric price cannot be negative.");
        }

        PricePerUnit = pricePerUnit;
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
