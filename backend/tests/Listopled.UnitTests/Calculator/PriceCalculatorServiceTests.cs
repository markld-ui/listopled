namespace Listopled.UnitTests.Calculator;

using Listopled.Domain.Calculator;
using Listopled.Domain.Calculator.Services;
using Listopled.Domain.Common;
using Xunit;

public sealed class PriceCalculatorServiceTests
{
    private readonly PriceCalculatorService _service = new();

    [Fact]
    public void Calculates_required_example_total_price()
    {
        PriceCalculationResult result = _service.Calculate(
            new BlanketSize("Взрослый", "150x220", 5, 0),
            new Fabric("Флис", 700, 0),
            new LeafShape("Дуб", 300, 0),
            new PriceCalculationSettings(4000),
            new Discount("Я новый подписчик Instagram", 300, 0));

        Assert.Equal(7500, result.TotalPrice);
    }

    [Fact]
    public void Calculates_total_price_without_discount()
    {
        PriceCalculationResult result = _service.Calculate(
            new BlanketSize("Взрослый", "150x220", 5, 0),
            new Fabric("Флис", 700, 0),
            new LeafShape("Дуб", 300, 0),
            new PriceCalculationSettings(4000));

        Assert.Equal(7800, result.TotalPrice);
        Assert.Equal(0, result.DiscountAmount);
    }

    [Fact]
    public void Large_discount_caps_total_price_at_zero()
    {
        PriceCalculationResult result = _service.Calculate(
            new BlanketSize("Детский", "100x150", 2, 0),
            new Fabric("Флис", 700, 0),
            new LeafShape("Дуб", 300, 0),
            new PriceCalculationSettings(4000),
            new Discount("Большая скидка", 10000, 0));

        Assert.Equal(0, result.TotalPrice);
    }

    [Fact]
    public void Inactive_size_is_rejected()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => _service.Calculate(
                new BlanketSize("Взрослый", "150x220", 5, 0, isActive: false),
                new Fabric("Флис", 700, 0),
                new LeafShape("Дуб", 300, 0),
                new PriceCalculationSettings(4000)));

        Assert.Contains("size", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Inactive_fabric_is_rejected()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => _service.Calculate(
                new BlanketSize("Взрослый", "150x220", 5, 0),
                new Fabric("Флис", 700, 0, isActive: false),
                new LeafShape("Дуб", 300, 0),
                new PriceCalculationSettings(4000)));

        Assert.Contains("fabric", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Inactive_leaf_shape_is_rejected()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => _service.Calculate(
                new BlanketSize("Взрослый", "150x220", 5, 0),
                new Fabric("Флис", 700, 0),
                new LeafShape("Дуб", 300, 0, isActive: false),
                new PriceCalculationSettings(4000)));

        Assert.Contains("leaf shape", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Inactive_discount_is_rejected()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => _service.Calculate(
                new BlanketSize("Взрослый", "150x220", 5, 0),
                new Fabric("Флис", 700, 0),
                new LeafShape("Дуб", 300, 0),
                new PriceCalculationSettings(4000),
                new Discount("Скидка", 300, 0, isActive: false)));

        Assert.Contains("discount", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Inactive_price_settings_are_rejected()
    {
        DomainException exception = Assert.Throws<DomainException>(
            () => _service.Calculate(
                new BlanketSize("Взрослый", "150x220", 5, 0),
                new Fabric("Флис", 700, 0),
                new LeafShape("Дуб", 300, 0),
                new PriceCalculationSettings(4000, isActive: false)));

        Assert.Contains("settings", exception.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Returns_full_price_breakdown()
    {
        PriceCalculationResult result = _service.Calculate(
            new BlanketSize("Взрослый", "150x220", 5, 0),
            new Fabric("Флис", 700, 0),
            new LeafShape("Дуб", 300, 0),
            new PriceCalculationSettings(4000),
            new Discount("Скидка", 300, 0));

        Assert.Equal(5, result.SizeCoefficient);
        Assert.Equal(700, result.FabricPricePerUnit);
        Assert.Equal(300, result.LeafShapeSurcharge);
        Assert.Equal(4000, result.BaseAmount);
        Assert.Equal(300, result.DiscountAmount);
        Assert.Equal(7500, result.TotalPrice);
        Assert.True(result.CalculatedAt <= DateTime.UtcNow);
    }
}
