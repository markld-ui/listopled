namespace Listopled.Domain.Calculator;

using Listopled.Domain.Common;

public sealed class BlanketSize : BaseEntity
{
    public BlanketSize(
        string name,
        string dimensions,
        decimal coefficient,
        int sortOrder,
        bool isActive = true)
    {
        SetName(name);
        SetDimensions(dimensions);
        SetCoefficient(coefficient);
        SetSortOrder(sortOrder);
        IsActive = isActive;
    }

    public string Name { get; private set; } = string.Empty;
    public string Dimensions { get; private set; } = string.Empty;
    public decimal Coefficient { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }

    public void Update(string name, string dimensions, decimal coefficient, int sortOrder)
    {
        SetName(name);
        SetDimensions(dimensions);
        SetCoefficient(coefficient);
        SetSortOrder(sortOrder);
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
            throw new DomainException("Size name is required.");
        }

        Name = name.Trim();
    }

    private void SetDimensions(string dimensions)
    {
        if (string.IsNullOrWhiteSpace(dimensions))
        {
            throw new DomainException("Size dimensions are required.");
        }

        Dimensions = dimensions.Trim();
    }

    private void SetCoefficient(decimal coefficient)
    {
        if (coefficient <= 0)
        {
            throw new DomainException("Size coefficient must be greater than zero.");
        }

        Coefficient = coefficient;
    }

    private void SetSortOrder(int sortOrder)
    {
        if (sortOrder < 0)
        {
            throw new DomainException("Sort order cannot be negative.");
        }

        SortOrder = sortOrder;
    }
}
