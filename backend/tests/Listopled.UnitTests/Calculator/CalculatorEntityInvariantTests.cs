namespace Listopled.UnitTests.Calculator;

using Listopled.Domain.Calculator;
using Listopled.Domain.Common;
using Xunit;

public sealed class CalculatorEntityInvariantTests
{
    [Fact]
    public void Cannot_create_size_with_non_positive_coefficient()
    {
        Assert.Throws<DomainException>(() => new BlanketSize("Adult", "150x220", 0, 0));
    }

    [Fact]
    public void Cannot_create_fabric_with_negative_price()
    {
        Assert.Throws<DomainException>(() => new Fabric("Fleece", -1, 0));
    }

    [Fact]
    public void Cannot_create_leaf_shape_with_negative_surcharge()
    {
        Assert.Throws<DomainException>(() => new LeafShape("Oak", -1, 0));
    }

    [Fact]
    public void Cannot_create_discount_with_negative_amount()
    {
        Assert.Throws<DomainException>(() => new Discount("New subscriber", -1, 0));
    }

    [Fact]
    public void Cannot_create_price_settings_with_negative_base_amount()
    {
        Assert.Throws<DomainException>(() => new PriceCalculationSettings(-1));
    }

    [Fact]
    public void Cannot_create_price_quote_with_negative_total_price()
    {
        Assert.Throws<DomainException>(
            () => new PriceQuote(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                null,
                1,
                700,
                300,
                4000,
                0,
                -1));
    }
}
