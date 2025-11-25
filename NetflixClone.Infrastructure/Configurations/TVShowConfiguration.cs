using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class TVShowConfiguration : IEntityTypeConfiguration<TVShow>
    {
        public void Configure(EntityTypeBuilder<TVShow> builder)
        {
            // Properties configuration
            builder.Property(t => t.CreatedBy)
                .HasMaxLength(200);

            builder.Property(t => t.Network)
                .HasMaxLength(200);

            builder.Property(t => t.Country)
                .HasMaxLength(100);

            builder.Property(t => t.Language)
                .HasMaxLength(100)
                .HasDefaultValue("en");

            // Indexes
            builder.HasIndex(t => t.IsOngoing);
            builder.HasIndex(t => t.FirstAirDate);
            builder.HasIndex(t => t.NumberOfSeasons);
        }
    }
}