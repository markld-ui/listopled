namespace Listopled.Infrastructure.Persistence.Configurations;

using Listopled.Domain.Calculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class BlanketSizeConfiguration : IEntityTypeConfiguration<BlanketSize>
{
    public void Configure(EntityTypeBuilder<BlanketSize> builder)
    {
        builder.ToTable("blanket_sizes");

        builder.HasKey(blanketSize => blanketSize.Id);

        builder.Property(blanketSize => blanketSize.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(blanketSize => blanketSize.Dimensions)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(blanketSize => blanketSize.Coefficient)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(blanketSize => blanketSize.IsActive)
            .IsRequired();

        builder.Property(blanketSize => blanketSize.SortOrder)
            .IsRequired();

        builder.Property(blanketSize => blanketSize.CreatedAt)
            .IsRequired();

        builder.Property(blanketSize => blanketSize.UpdatedAt);

        builder.HasIndex(blanketSize => blanketSize.IsActive);
        builder.HasIndex(blanketSize => blanketSize.SortOrder);
    }
}
