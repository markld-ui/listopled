namespace Listopled.Domain.Calculator;

using Listopled.Domain.Common;

public sealed class PriceQuote : BaseEntity
{
    public PriceQuote(
        Guid blanketSizeId,
        Guid fabricId,
        Guid leafShapeId,
        Guid? discountId,
        decimal sizeCoefficient,
        decimal fabricPricePerUnit,
        decimal leafShapeSurcharge,
        decimal baseAmount,
        decimal discountAmount,
        decimal totalPrice,
        DateTime? calculatedAt = null)
    {
        ValidateRequiredId(blanketSizeId, "Blanket size id is required.");
        ValidateRequiredId(fabricId, "Fabric id is required.");
        ValidateRequiredId(leafShapeId, "Leaf shape id is required.");
        ValidatePositive(sizeCoefficient, "Size coefficient must be greater than zero.");
        ValidateNotNegative(fabricPricePerUnit, "Fabric price cannot be negative.");
        ValidateNotNegative(leafShapeSurcharge, "Leaf shape surcharge cannot be negative.");
        ValidateNotNegative(baseAmount, "Base amount cannot be negative.");
        ValidateNotNegative(discountAmount, "Discount amount cannot be negative.");
        ValidateNotNegative(totalPrice, "Total price cannot be negative.");

        BlanketSizeId = blanketSizeId;
        FabricId = fabricId;
        LeafShapeId = leafShapeId;
        DiscountId = discountId;
        SizeCoefficient = sizeCoefficient;
        FabricPricePerUnit = fabricPricePerUnit;
        LeafShapeSurcharge = leafShapeSurcharge;
        BaseAmount = baseAmount;
        DiscountAmount = discountAmount;
        TotalPrice = totalPrice;
        CalculatedAt = calculatedAt ?? DateTime.UtcNow;
    }

    public Guid BlanketSizeId { get; private set; }
    public Guid FabricId { get; private set; }
    public Guid LeafShapeId { get; private set; }
    public Guid? DiscountId { get; private set; }
    public decimal SizeCoefficient { get; private set; }
    public decimal FabricPricePerUnit { get; private set; }
    public decimal LeafShapeSurcharge { get; private set; }
    public decimal BaseAmount { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime CalculatedAt { get; private set; }

    private static void ValidateRequiredId(Guid value, string message)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException(message);
        }
    }

    private static void ValidatePositive(decimal value, string message)
    {
        if (value <= 0)
        {
            throw new DomainException(message);
        }
    }

    private static void ValidateNotNegative(decimal value, string message)
    {
        if (value < 0)
        {
            throw new DomainException(message);
        }
    }
}
