using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EFC.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<BabyChore> BabyChores => Set<BabyChore>();
    public DbSet<ChoreCompletion> ChoreCompletions => Set<ChoreCompletion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExternalId).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Provider).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.HasIndex(e => new { e.ExternalId, e.Provider }).IsUnique();
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired();
            entity.HasIndex(e => e.Parent1Id);
            entity.HasIndex(e => e.Parent2Id);
        });

        modelBuilder.Entity<BabyChore>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.PointValue).IsRequired();
            entity.Property(e => e.Category).IsRequired();
            entity.HasIndex(e => e.ContractId);
            entity.HasIndex(e => e.CreatedByParentId);
        });

        modelBuilder.Entity<ChoreCompletion>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PointsAwarded).IsRequired();
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.HasIndex(e => e.ChoreId);
            entity.HasIndex(e => e.CompletedByParentId);
        });
    }
}
