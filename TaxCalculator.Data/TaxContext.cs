using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Data;

public class TaxContext : DbContext
{
    public DbSet<TaxProfile> TaxProfiles { get; set; }

    public DbSet<Tax> Taxes { get; set; }

    public DbSet<AdditionalSpend> AdditionalSpends { get; set; }
    
    public DbSet<Income> Incomes { get; set; }

    public DbSet<Currency> Currencies { get; set; }
    
    public TaxContext()
    {
        
    }

    public TaxContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaxProfile>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Tax>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<AdditionalSpend>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Income>().HasQueryFilter(x => !x.IsDeleted);
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var deletedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted).ToList();
        foreach (var deletedEntry in deletedEntries)
        {
            ((BaseEntity) deletedEntry.Entity).IsDeleted = true;
            deletedEntry.State = EntityState.Modified;
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}