using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.EntityConfigurations;

public class AlbumConfiguration  : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).HasMaxLength(30).IsRequired();
        builder.Property(c => c.ReleaseYear).HasMaxLength(4).IsRequired().HasDefaultValue(1900);
    }
}
