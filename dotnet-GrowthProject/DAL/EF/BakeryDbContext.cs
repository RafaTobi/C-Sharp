using System.Diagnostics;
using Groeiproject.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Groeiproject.DAL.EF;

public class BakeryDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Baker> Bakers { get; set; }
    public DbSet<Bakery> Bakeries { get; set; }
    public DbSet<Bread> Breads { get; set; }
    public DbSet<Contract> Contracts { get; set; }

    public BakeryDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bakery>()
            .HasMany(bakery => bakery.Contracts)
            .WithOne(contract => contract.Bakery)
            .IsRequired();

        modelBuilder.Entity<Baker>()
            .HasMany(baker => baker.Contracts)
            .WithOne(contract => contract.Baker)
            .IsRequired();
        
        modelBuilder.Entity<Bakery>()
            .HasMany(bakery => bakery.Breads)
            .WithOne(bread => bread.Bakery)
            .HasForeignKey("BakeryId")
            .IsRequired(false);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\GroeiprojectDatabase.db");
        }

        optionsBuilder.LogTo(logMessage => Debug.WriteLine(logMessage), LogLevel.Information);
    }

    public bool CreateDatabase(bool removeDatabase)
    {
        if (removeDatabase)
        {
            Database.EnsureDeleted();
        }
        
        return Database.EnsureCreated();
    }
}