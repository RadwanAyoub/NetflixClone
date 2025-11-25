using Microsoft.EntityFrameworkCore;
using NetflixClone.Application.Interfaces.Repositories;
using NetflixClone.Domain.Entities;
using NetflixClone.Infrastructure.Data;

namespace NetflixClone.Infrastructure.Data.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly ApplicationDbContext _context;

        public ContentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Content?> GetByIdAsync(int id)
        {
            return await _context.Set<Content>()
                .Include(c => c.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<IEnumerable<Content>> GetAllAsync()
        {
            return await _context.Set<Content>()
                .Include(c => c.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Content>> SearchAsync(string searchTerm)
        {
            return await _context.Set<Content>()
                .Include(c => c.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(c => (c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)) && !c.IsDeleted)
                .OrderByDescending(c => c.Rating)
                .ToListAsync();
        }

        public async Task<IEnumerable<Content>> GetByGenreIdAsync(int genreId)
        {
            return await _context.Set<Content>()
                .Include(c => c.ContentGenres)
                .ThenInclude(cg => cg.Genre)
                .Where(c => c.ContentGenres.Any(cg => cg.GenreId == genreId) && !c.IsDeleted)
                .OrderByDescending(c => c.Rating)
                .ToListAsync();
        }

        public async Task<Content> AddAsync(Content content)
        {
            _context.Set<Content>().Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task UpdateAsync(Content content)
        {
            _context.Set<Content>().Update(content);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Content content)
        {
            content.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}