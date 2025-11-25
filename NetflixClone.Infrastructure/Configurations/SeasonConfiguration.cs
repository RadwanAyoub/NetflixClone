using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class SeasonConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            // Table name
            builder.ToTable("Seasons");

            // Primary Key
            builder.HasKey(s => s.Id);

            // Properties configuration
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Overview)
                .HasMaxLength(2000);

            builder.Property(s => s.PosterUrl)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(s => s.TVShow)
                .WithMany(t => t.Seasons)
                .HasForeignKey(s => s.TVShowId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite unique constraint - season numbers must be unique per TV show
            builder.HasIndex(s => new { s.TVShowId, s.SeasonNumber }).IsUnique();

            // Query filter for soft delete
            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}