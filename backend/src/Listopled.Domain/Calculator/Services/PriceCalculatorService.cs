namespace Listopled.Domain.Calculator.Services;

using Listopled.Domain.Common;

public sealed class PriceCalculatorService
{
    public PriceCalculationResult Calculate(
        BlanketSize blanketSize,
        Fabric fabric,
        LeafShape leafShape,
        PriceCalculationSettings settings,
        Discount? discount = null)
    {
        ArgumentNullException.ThrowIfNull(blanketSize);
        ArgumentNullException.ThrowIfNull(fabric);
        ArgumentNullException.ThrowIfNull(leafShape);
        ArgumentNullException.ThrowIfNull(settings);

        EnsureActive(blanketSize.IsActive, "Selected blanket size is inactive.");
        EnsureActive(fabric.IsActive, "Selected fabric is inactive.");
        EnsureActive(leafShape.IsActive, "Selected leaf shape is inactive.");
        EnsureActive(settings.IsActive, "Price calculation settings are inactive.");

        if (discount is not null)
        {
            EnsureActive(discount.IsActive, "Selected discount is inactive.");
        }

        decimal discountAmount = discount?.Amount ?? 0;
        decimal rawTotal = leafShape.Surcharge
            + fabric.PricePerUnit * blanketSize.Coefficient
            + settings.BaseAmount
            - discountAmount;

        decimal totalPrice = Math.Max(0, rawTotal);

        return new PriceCalculationResult(
            blanketSize.Coefficient,
            fabric.PricePerUnit,
            leafShape.Surcharge,
            settings.BaseAmount,
            discountAmount,
            totalPrice,
            DateTime.UtcNow);
    }

    private static void EnsureActive(bool isActive, string message)
    {
        if (!isActive)
        {
            throw new DomainException(message);
        }
    }
}
