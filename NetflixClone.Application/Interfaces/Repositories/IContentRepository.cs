using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Interfaces.Repositories
{
    public interface IContentRepository
    {
        Task<Content?> GetByIdAsync(int id);
        Task<IEnumerable<Content>> GetAllAsync();
        Task<IEnumerable<Content>> SearchAsync(string searchTerm);
        Task<IEnumerable<Content>> GetByGenreIdAsync(int genreId);
        Task<Content> AddAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(Content content);
    }
}