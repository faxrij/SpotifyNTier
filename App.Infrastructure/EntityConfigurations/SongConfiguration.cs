using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).HasMaxLength(4).IsRequired();
        builder.Property(c => c.Lyrics).HasMaxLength(400).IsRequired();
        builder.Property(c => c.DurationInSeconds).HasMaxLength(4).IsRequired();
    }
}