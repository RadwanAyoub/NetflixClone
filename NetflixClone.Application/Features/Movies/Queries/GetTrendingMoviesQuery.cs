using MediatR;
using NetflixClone.Application.DTOs;

namespace NetflixClone.Application.Features.Movies.Queries
{
    public record GetTrendingMoviesQuery(int Count = 10) : IRequest<IEnumerable<MovieDto>>;
}

