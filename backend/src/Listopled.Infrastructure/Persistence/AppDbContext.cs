namespace Listopled.Infrastructure.Persistence;

using Listopled.Domain.Calculator;
using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<BlanketSize> BlanketSizes => Set<BlanketSize>();
    public DbSet<Fabric> Fabrics => Set<Fabric>();
    public DbSet<LeafShape> LeafShapes => Set<LeafShape>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<PriceCalculationSettings> PriceCalculationSettings => Set<PriceCalculationSettings>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
