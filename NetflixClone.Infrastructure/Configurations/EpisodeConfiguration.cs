using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            // Table name
            builder.ToTable("Episodes");

            // Primary Key
            builder.HasKey(e => e.Id);

            // Properties configuration
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Overview)
                .HasMaxLength(2000);

            builder.Property(e => e.VideoUrl)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(e => e.Season)
                .WithMany(s => s.Episodes)
                .HasForeignKey(e => e.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite unique constraint - episode numbers must be unique per season
            builder.HasIndex(e => new { e.SeasonId, e.EpisodeNumber }).IsUnique();

            // Query filter for soft delete
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}