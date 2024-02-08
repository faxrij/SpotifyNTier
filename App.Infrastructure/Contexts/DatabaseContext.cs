using System.Reflection;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Contexts;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
    }

    public DbSet<Song> Songs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Singer> Singers { get; set; }
    public DbSet<Album> Albums { get; set; }
    
    public override int SaveChanges()
    {
        AddTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is Auditable && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;

            if (entity.State == EntityState.Added)
            {
                ((Auditable)entity.Entity).CreatedAt = now;
            }
            ((Auditable)entity.Entity).ModifiedAt = now;
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyAllConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}