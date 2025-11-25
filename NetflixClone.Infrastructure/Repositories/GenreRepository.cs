using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces.Repositories;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.Infrastructure.Data.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Contents)
                .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
        }

        public async Task<Genre?> GetByNameAsync(string name)
        {
            return await _context.Genres
                .Include(g => g.Contents)
                .FirstOrDefaultAsync(g => g.Name == name && !g.IsDeleted);
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres
                .Include(g => g.Contents)
                .Where(g => !g.IsDeleted)
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Genre>> GetPopularAsync(int count = 10)
        {
            return await _context.Genres
                .Include(g => g.Contents)
                .Where(g => !g.IsDeleted)
                .OrderByDescending(g => g.Contents.Count)
                .Take(count)
                .ToListAsync();
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Genre genre)
        {
            genre.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Genres
                .AnyAsync(g => g.Id == id && !g.IsDeleted);
        }

        public async Task<bool> NameExistsAsync(string name)
        {
            return await _context.Genres
                .AnyAsync(g => g.Name == name && !g.IsDeleted);
        }
    }
}