using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;
using NetflixClone.Domain.Enums;

namespace NetflixClone.Infrastructure.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            // Table name
            builder.ToTable("Contents");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties configuration
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Overview)
                .HasMaxLength(2000);

            builder.Property(c => c.PosterUrl)
                .HasMaxLength(500);

            builder.Property(c => c.BackdropUrl)
                .HasMaxLength(500);

            builder.Property(c => c.TrailerUrl)
                .HasMaxLength(500);

            builder.Property(c => c.VideoUrl)
                .HasMaxLength(500);

            builder.Property(c => c.ExternalId)
                .HasMaxLength(100);

            builder.Property(c => c.Rating)
                .HasPrecision(3, 1);

            builder.Property(c => c.ContentType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            builder.Property(c => c.MaxQuality)
                .HasConversion<string>()
                .HasMaxLength(20);

            // Indexes
            builder.HasIndex(c => c.ContentType);
            builder.HasIndex(c => c.IsTrending);
            builder.HasIndex(c => c.IsFeatured);
            builder.HasIndex(c => c.ReleaseDate);
            builder.HasIndex(c => c.Rating);
            builder.HasIndex(c => c.ExternalId).IsUnique();

            // Query filter for soft delete
            builder.HasQueryFilter(c => !c.IsDeleted);

            // Relationships will be configured in derived types
        }
    }
}