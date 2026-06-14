namespace Listopled.Infrastructure.Persistence.Configurations;

using Listopled.Domain.Calculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("discounts");

        builder.HasKey(discount => discount.Id);

        builder.Property(discount => discount.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(discount => discount.Description)
            .HasMaxLength(1000);

        builder.Property(discount => discount.Amount)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(discount => discount.IsActive)
            .IsRequired();

        builder.Property(discount => discount.SortOrder)
            .IsRequired();

        builder.Property(discount => discount.CreatedAt)
            .IsRequired();

        builder.Property(discount => discount.UpdatedAt);

        builder.HasIndex(discount => discount.IsActive);
        builder.HasIndex(discount => discount.SortOrder);
    }
}
