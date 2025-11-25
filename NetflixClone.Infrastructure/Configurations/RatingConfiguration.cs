using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            // Table name
            builder.ToTable("Ratings");

            // Primary Key
            builder.HasKey(r => r.Id);

            // Properties configuration
            builder.Property(r => r.Score)
                .IsRequired();

            builder.Property(r => r.Comment)
                .HasMaxLength(1000);

            // Relationships
            builder.HasOne(r => r.UserProfile)
                .WithMany()
                .HasForeignKey(r => r.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.Content)
                .WithMany(c => c.Ratings)
                .HasForeignKey(r => r.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite unique constraint - a user profile can only rate a content once
            builder.HasIndex(r => new { r.UserProfileId, r.ContentId }).IsUnique();

            // Check constraint for score range (provider-agnostic, avoids warnings)
            builder.HasCheckConstraint("CK_Ratings_Score", $"{nameof(Rating.Score)} >= 1 AND {nameof(Rating.Score)} <= 5");

            // Query filter for soft delete
            builder.HasQueryFilter(r => !r.IsDeleted);
        }
    }
}