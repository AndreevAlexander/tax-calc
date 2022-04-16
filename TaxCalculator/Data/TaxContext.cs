using Microsoft.EntityFrameworkCore;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Data;

public class TaxContext : DbContext
{
    public DbSet<TaxProfile> TaxProfiles { get; set; }

    public DbSet<Tax> Taxes { get; set; }

    public DbSet<AdditionalSpend> AdditionalSpends { get; set; }

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
        base.OnModelCreating(modelBuilder);
    }
}