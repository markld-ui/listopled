namespace Listopled.Infrastructure.Persistence.Configurations;

using Listopled.Domain.Calculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class PriceCalculationSettingsConfiguration : IEntityTypeConfiguration<PriceCalculationSettings>
{
    public void Configure(EntityTypeBuilder<PriceCalculationSettings> builder)
    {
        builder.ToTable("price_calculation_settings");

        builder.HasKey(settings => settings.Id);

        builder.Property(settings => settings.BaseAmount)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(settings => settings.IsActive)
            .IsRequired();

        builder.Property(settings => settings.CreatedAt)
            .IsRequired();

        builder.Property(settings => settings.UpdatedAt);

        builder.HasIndex(settings => settings.IsActive);
    }
}
