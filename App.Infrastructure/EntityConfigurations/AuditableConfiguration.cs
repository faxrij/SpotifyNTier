// using App.Domain.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
//
// namespace App.Infrastructure.EntityConfigurations;
//
// public class AuditableConfiguration : IEntityTypeConfiguration<Auditable>
// {
//     public void Configure(EntityTypeBuilder<Auditable> builder)
//     {
//         builder.Property(j => j.CreatedAt).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
//     }
// }