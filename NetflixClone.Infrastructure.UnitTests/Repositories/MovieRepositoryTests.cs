using Microsoft.EntityFrameworkCore;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;
using NetflixClone.Infrastructure.Data.Repositories;

namespace NetflixClone.Infrastructure.UnitTests.Repositories
{
    public class MovieRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly MovieRepository _repository;

        public MovieRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new MovieRepository(_context);

            // Seed test data
            SeedTestData();
        }

        private void SeedTestData()
        {
            var movies = new List<Movie>
            {
                new Movie("The Matrix", "Sci-Fi movie") 
                { 
                    Rating = 8.7m, 
                    IsTrending = true,
                    ReleaseDate = DateTime.UtcNow.AddMonths(-2)
                },
                new Movie("Inception", "Mind-bending thriller") 
                { 
                    Rating = 8.8m, 
                    IsFeatured = true,
                    ReleaseDate = DateTime.UtcNow.AddYears(-5)
                },
                new Movie("The Shawshank Redemption", "Prison drama") 
                { 
                    Rating = 9.3m,
                    ReleaseDate = DateTime.UtcNow.AddYears(-15)
                }
            };

            _context.Movies.AddRange(movies);
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetByIdAsync_WithValidId_ReturnsMovie()
        {
            // Act
            var movie = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(movie);
            Assert.Equal("The Matrix", movie.Title);
        }

        [Fact]
        public async Task GetTrendingAsync_ReturnsTrendingMovies()
        {
            // Act
            var trendingMovies = await _repository.GetTrendingAsync(10);

            // Assert
            Assert.Single(trendingMovies);
            Assert.All(trendingMovies, m => Assert.True(m.IsTrending));
        }

        [Fact]
        public async Task GetRecentReleasesAsync_ReturnsRecentMovies()
        {
            // Act
            var recentMovies = await _repository.GetRecentReleasesAsync(6);

            // Assert
            Assert.Single(recentMovies);
            Assert.All(recentMovies, m => Assert.True(m.ReleaseDate >= DateTime.UtcNow.AddMonths(-6)));
        }

        [Fact]
        public async Task DeleteAsync_SoftDeletesMovie()
        {
            // Arrange
            var movie = await _repository.GetByIdAsync(1);

            // Act
            await _repository.DeleteAsync(movie!);
            var deletedMovie = await _repository.GetByIdAsync(1);

            // Assert
            Assert.Null(deletedMovie);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}