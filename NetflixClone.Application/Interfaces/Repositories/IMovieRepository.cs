using NetflixClone.Domain.Entities;

namespace NetflixClone.Application.Interfaces.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetByIdAsync(int id);
        Task<Movie?> GetByIdWithGenresAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<IEnumerable<Movie>> GetTrendingAsync(int count = 10);
        Task<IEnumerable<Movie>> GetFeaturedAsync();
        Task<IEnumerable<Movie>> GetByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetByDirectorAsync(string director);
        Task<IEnumerable<Movie>> GetRecentReleasesAsync(int months = 6);
        Task<IEnumerable<Movie>> GetClassicsAsync();
        Task<Movie> AddAsync(Movie movie);
        Task UpdateAsync(Movie movie);
        Task DeleteAsync(Movie movie);
        Task<bool> ExistsAsync(int id);
        Task<int> GetCountAsync();
    }
}