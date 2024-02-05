using App.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public class SongConfiguration : BaseEntityConfiguration<Song>
{
    public override void Configure(EntityTypeBuilder<Song> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.Title).HasMaxLength(10).IsRequired();
        builder.Property(c => c.Lyrics).HasMaxLength(50).IsRequired();
        builder.Property(c => c.DurationInSeconds).HasMaxLength(4).IsRequired();
    }
}