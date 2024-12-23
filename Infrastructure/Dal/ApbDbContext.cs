using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Dal;

/// <summary>
/// Контекст базы данных
/// </summary>
public class ApbDbContext : DbContext
{
    public DbSet<Card> Cards { get; init; }
    public DbSet<Transaction> Transactions { get; init; }
    public DbSet<User> Users { get; init; }
    
    public ApbDbContext(DbContextOptions<ApbDbContext> options) : base(options)
    {
    }

    public ApbDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseNpgsql(
            "User ID=postgres;Password=7733;Host=host.docker.internal;Port=5432;Database=ApbDatabase;");
    }

    /// <summary>
    /// Метод применения конфигураций
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Currency>();
        modelBuilder.HasPostgresEnum<TransactionType>();
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApbDbContext).Assembly);
    }
}