using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            // Table name
            builder.ToTable("UserProfiles");

            // Primary Key
            builder.HasKey(up => up.Id);

            // Properties configuration
            builder.Property(up => up.ProfileName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(up => up.AvatarUrl)
                .HasMaxLength(500);

            builder.Property(up => up.Language)
                .HasMaxLength(10)
                .HasDefaultValue("en");

            // Relationships
            builder.HasOne(up => up.User)
                .WithMany(u => u.UserProfiles)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many relationship with Content (WatchHistory)
            builder.HasMany(up => up.WatchHistory)
                .WithMany(c => c.UserProfiles)
                .UsingEntity<Dictionary<string, object>>(
                    "UserProfileWatchHistory",
                    j => j.HasOne<Content>().WithMany().HasForeignKey("ContentId"),
                    j => j.HasOne<UserProfile>().WithMany().HasForeignKey("UserProfileId"),
                    j =>
                    {
                        j.HasKey("UserProfileId", "ContentId");
                        j.ToTable("UserProfileWatchHistory");
                        j.Property<DateTime>("WatchedAt").HasDefaultValueSql("GETUTCDATE()");
                    });

            // Indexes
            builder.HasIndex(up => new { up.UserId, up.ProfileName }).IsUnique();
            builder.HasIndex(up => up.IsKidsProfile);
            builder.HasIndex(up => up.IsActive);

            // Query filter for soft delete and active profiles
            builder.HasQueryFilter(up => !up.IsDeleted && up.IsActive);
        }
    }
}