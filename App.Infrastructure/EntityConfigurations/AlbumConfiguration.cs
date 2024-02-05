using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public class AlbumConfiguration : BaseEntityConfiguration<Album>
{
    public override void Configure(EntityTypeBuilder<Album> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.Title).HasMaxLength(30).IsRequired();
        builder.Property(c => c.ReleaseYear).HasMaxLength(4).IsRequired().HasDefaultValue(1900);
    }
}
