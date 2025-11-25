using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Features.Genres.Queries
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GenreDto>>
    {
        // Can add filters/pagination later
    }
}