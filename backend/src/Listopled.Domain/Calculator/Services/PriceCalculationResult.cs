namespace Listopled.Domain.Calculator.Services;

public sealed class PriceCalculationResult
{
    public PriceCalculationResult(
        decimal sizeCoefficient,
        decimal fabricPricePerUnit,
        decimal leafShapeSurcharge,
        decimal baseAmount,
        decimal discountAmount,
        decimal totalPrice,
        DateTime calculatedAt)
    {
        SizeCoefficient = sizeCoefficient;
        FabricPricePerUnit = fabricPricePerUnit;
        LeafShapeSurcharge = leafShapeSurcharge;
        BaseAmount = baseAmount;
        DiscountAmount = discountAmount;
        TotalPrice = totalPrice;
        CalculatedAt = calculatedAt;
    }

    public decimal SizeCoefficient { get; }
    public decimal FabricPricePerUnit { get; }
    public decimal LeafShapeSurcharge { get; }
    public decimal BaseAmount { get; }
    public decimal DiscountAmount { get; }
    public decimal TotalPrice { get; }
    public DateTime CalculatedAt { get; }
}
