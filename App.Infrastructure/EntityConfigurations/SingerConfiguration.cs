using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public class SingerConfiguration : IEntityTypeConfiguration<Singer>
{
    public void Configure(EntityTypeBuilder<Singer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.BirthDate).HasMaxLength(10).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
    }
}