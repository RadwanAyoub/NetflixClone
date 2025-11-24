using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre?> GetByIdAsync(int id);
        Task<Genre?> GetByNameAsync(string name);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task<IEnumerable<Genre>> GetPopularAsync(int count = 10);
        Task<Genre> AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(Genre genre);
        Task<bool> ExistsAsync(int id);
        Task<bool> NameExistsAsync(string name);
    }
}