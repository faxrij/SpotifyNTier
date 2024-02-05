using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey("Id"); 
        var createdAtProperty = builder.Property<DateTime>("CreatedAt");
        createdAtProperty.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}