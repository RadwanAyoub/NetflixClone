using Microsoft.EntityFrameworkCore;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.WebApi
{
    public static class WebApplicationExtensions
    {
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
                
                // Optional: Seed initial data
                //await SeedDataAsync(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }
        }

        private static async Task SeedDataAsync(ApplicationDbContext context)
        {
            // Add initial genres if none exist
            if (!await context.Genres.AnyAsync())
            {
                var genres = new[]
                {
                    new Domain.Entities.Genre("Action", "Exciting action-packed content"),
                    new Domain.Entities.Genre("Comedy", "Funny and humorous content"),
                    new Domain.Entities.Genre("Drama", "Emotional and serious storytelling"),
                    new Domain.Entities.Genre("Sci-Fi", "Science fiction and futuristic content"),
                    new Domain.Entities.Genre("Horror", "Scary and thrilling content"),
                    new Domain.Entities.Genre("Documentary", "Real-life educational content")
                };

                await context.Genres.AddRangeAsync(genres);
                await context.SaveChangesAsync();
            }
        }
    }
}