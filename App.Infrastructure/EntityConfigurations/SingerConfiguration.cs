using App.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

internal class SingerConfiguration : BaseEntityConfiguration<Singer>
{
    public override void Configure(EntityTypeBuilder<Singer> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.BirthDate).HasMaxLength(10).IsRequired();
        builder.Property(c => c.Name).HasMaxLength(20).IsRequired();
    }
}