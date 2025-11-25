using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class WatchlistItemConfiguration : IEntityTypeConfiguration<WatchlistItem>
    {
        public void Configure(EntityTypeBuilder<WatchlistItem> builder)
        {
            // Table name
            builder.ToTable("WatchlistItems");

            // Primary Key
            builder.HasKey(wi => wi.Id);

            // Properties configuration
            builder.Property(wi => wi.AddedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(wi => wi.UserProfile)
                .WithMany(up => up.Watchlist)
                .HasForeignKey(wi => wi.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(wi => wi.Content)
                .WithMany()
                .HasForeignKey(wi => wi.ContentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite unique constraint - a content can only be in a user's watchlist once
            builder.HasIndex(wi => new { wi.UserProfileId, wi.ContentId }).IsUnique();

            // Query filter for soft delete
            builder.HasQueryFilter(wi => !wi.IsDeleted);
        }
    }
}