using Microsoft.EntityFrameworkCore;
using NetflixClone.Domain.Common;
using NetflixClone.Domain.Entities;
using System.Reflection;

namespace NetflixClone.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets for our entities
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<TVShow> TVShows => Set<TVShow>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<ContentGenre> ContentGenres => Set<ContentGenre>();
        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<WatchlistItem> WatchlistItems => Set<WatchlistItem>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Season> Seasons => Set<Season>();
        public DbSet<Episode> Episodes => Set<Episode>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the Configurations assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure the TPH (Table Per Hierarchy) inheritance for Content
            modelBuilder.Entity<Content>()
                .HasDiscriminator<Domain.Enums.ContentType>("ContentType")
                .HasValue<Movie>(Domain.Enums.ContentType.Movie)
                .HasValue<TVShow>(Domain.Enums.ContentType.TVShow);

            // Ignore DomainEvent - it's not a database entity
            modelBuilder.Ignore<DomainEvent>();

            // Additional global configurations
            ConfigureGlobalFilters(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Auto-set timestamps
            SetTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            // Auto-set timestamps
            SetTimestamps();
            return base.SaveChanges();
        }

        private void SetTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is EntityBase && 
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (EntityBase)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        private static void ConfigureGlobalFilters(ModelBuilder modelBuilder)
        {
            // Soft delete filter - automatically filter out deleted entities
            modelBuilder.Entity<Content>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Genre>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(e => !e.IsDeleted && e.IsActive);
            modelBuilder.Entity<UserProfile>().HasQueryFilter(e => !e.IsDeleted && e.IsActive);
            modelBuilder.Entity<ContentGenre>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Rating>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<WatchlistItem>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Season>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Episode>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}