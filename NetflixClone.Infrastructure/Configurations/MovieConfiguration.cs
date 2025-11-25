using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Properties configuration
            builder.Property(m => m.Director)
                .HasMaxLength(200);

            builder.Property(m => m.Writers)
                .HasMaxLength(500);

            builder.Property(m => m.Cast)
                .HasMaxLength(1000);

            builder.Property(m => m.ProductionCompany)
                .HasMaxLength(200);

            builder.Property(m => m.Country)
                .HasMaxLength(100);

            builder.Property(m => m.Language)
                .HasMaxLength(100)
                .HasDefaultValue("en");

            builder.Property(m => m.Budget)
                .HasPrecision(18, 2);

            builder.Property(m => m.Revenue)
                .HasPrecision(18, 2);

            // Self-referencing relationship for sequels
            builder.HasOne(m => m.SequelTo)
                .WithMany(m => m.Sequels)
                .HasForeignKey(m => m.SequelToId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(m => m.Director);
            builder.HasIndex(m => m.ReleaseDate);
            builder.HasIndex(m => m.Revenue);
        }
    }
}