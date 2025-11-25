using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            // Table name
            builder.ToTable("Genres");

            // Primary Key
            builder.HasKey(g => g.Id);

            // Properties configuration
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(g => g.Description)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(g => g.Name).IsUnique();

            // Query filter for soft delete
            builder.HasQueryFilter(g => !g.IsDeleted);
        }
    }
}