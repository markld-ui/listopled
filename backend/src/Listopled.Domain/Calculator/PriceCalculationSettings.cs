namespace Listopled.Domain.Calculator;

using Listopled.Domain.Common;

public sealed class PriceCalculationSettings : BaseEntity
{
    public PriceCalculationSettings(decimal baseAmount, bool isActive = true)
    {
        SetBaseAmount(baseAmount);
        IsActive = isActive;
    }

    public decimal BaseAmount { get; private set; }
    public bool IsActive { get; private set; }

    public void UpdateBaseAmount(decimal baseAmount)
    {
        SetBaseAmount(baseAmount);
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

    private void SetBaseAmount(decimal baseAmount)
    {
        if (baseAmount < 0)
        {
            throw new DomainException("Base amount cannot be negative.");
        }

        BaseAmount = baseAmount;
    }
}
