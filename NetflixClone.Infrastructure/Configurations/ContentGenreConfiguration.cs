using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
    {
        public void Configure(EntityTypeBuilder<ContentGenre> builder)
        {
            // Table name
            builder.ToTable("ContentGenres");

            // Primary Key
            builder.HasKey(cg => cg.Id);

            // Composite unique constraint - a content can't have the same genre twice
            builder.HasIndex(cg => new { cg.ContentId, cg.GenreId }).IsUnique();

            // Relationships
            builder.HasOne(cg => cg.Content)
                .WithMany(c => c.ContentGenres)
                .HasForeignKey(cg => cg.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cg => cg.Genre)
                .WithMany(g => g.ContentGenres)
                .HasForeignKey(cg => cg.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            // Query filter for soft delete
            builder.HasQueryFilter(cg => !cg.IsDeleted);
        }
    }
}