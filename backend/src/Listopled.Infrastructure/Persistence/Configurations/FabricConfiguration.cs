namespace Listopled.Infrastructure.Persistence.Configurations;

using Listopled.Domain.Calculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class FabricConfiguration : IEntityTypeConfiguration<Fabric>
{
    public void Configure(EntityTypeBuilder<Fabric> builder)
    {
        builder.ToTable("fabrics");

        builder.HasKey(fabric => fabric.Id);

        builder.Property(fabric => fabric.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(fabric => fabric.Description)
            .HasMaxLength(1000);

        builder.Property(fabric => fabric.PricePerUnit)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(fabric => fabric.IsActive)
            .IsRequired();

        builder.Property(fabric => fabric.SortOrder)
            .IsRequired();

        builder.Property(fabric => fabric.CreatedAt)
            .IsRequired();

        builder.Property(fabric => fabric.UpdatedAt);

        builder.HasIndex(fabric => fabric.IsActive);
        builder.HasIndex(fabric => fabric.SortOrder);
    }
}
