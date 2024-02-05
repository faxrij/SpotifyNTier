using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Auditable
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey("Id"); 
        builder.Property(j => j.CreatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}