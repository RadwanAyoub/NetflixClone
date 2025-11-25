using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces.Repositories;
using NetflixClone.Domain.Entities;

namespace NetflixClone.Infrastructure.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<Movie?> GetByIdWithGenresAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => !m.IsDeleted)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetTrendingAsync(int count = 10)
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => m.IsTrending && !m.IsDeleted)
                .OrderByDescending(m => m.Rating)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetFeaturedAsync()
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => m.IsFeatured && !m.IsDeleted)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByGenreAsync(string genre)
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => m.ContentGenres.Any(cg => cg.Genre.Name == genre) && !m.IsDeleted)
                .OrderByDescending(m => m.Rating)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetByDirectorAsync(string director)
        {
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => m.Director.Contains(director) && !m.IsDeleted)
                .OrderByDescending(m => m.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetRecentReleasesAsync(int months = 6)
        {
            var cutoffDate = DateTime.UtcNow.AddMonths(-months);
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => m.ReleaseDate >= cutoffDate && !m.IsDeleted)
                .OrderByDescending(m => m.ReleaseDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetClassicsAsync()
        {
            var cutoffDate = DateTime.UtcNow.AddYears(-10);
            return await _context.Movies
                .Include(m => m.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(m => m.ReleaseDate <= cutoffDate && !m.IsDeleted)
                .OrderByDescending(m => m.Rating)
                .ToListAsync();
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Movie movie)
        {
            movie.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Movies
                .AnyAsync(m => m.Id == id && !m.IsDeleted);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Movies
                .CountAsync(m => !m.IsDeleted);
        }
    }
}