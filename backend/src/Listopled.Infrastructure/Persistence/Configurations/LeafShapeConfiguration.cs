namespace Listopled.Infrastructure.Persistence.Configurations;

using Listopled.Domain.Calculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class LeafShapeConfiguration : IEntityTypeConfiguration<LeafShape>
{
    public void Configure(EntityTypeBuilder<LeafShape> builder)
    {
        builder.ToTable("leaf_shapes");

        builder.HasKey(leafShape => leafShape.Id);

        builder.Property(leafShape => leafShape.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(leafShape => leafShape.Description)
            .HasMaxLength(1000);

        builder.Property(leafShape => leafShape.Surcharge)
            .HasPrecision(12, 2)
            .IsRequired();

        builder.Property(leafShape => leafShape.IsActive)
            .IsRequired();

        builder.Property(leafShape => leafShape.SortOrder)
            .IsRequired();

        builder.Property(leafShape => leafShape.CreatedAt)
            .IsRequired();

        builder.Property(leafShape => leafShape.UpdatedAt);

        builder.HasIndex(leafShape => leafShape.IsActive);
        builder.HasIndex(leafShape => leafShape.SortOrder);
    }
}
